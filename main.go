package main

import (
	"fmt"
	"net/http"
	"time"
)

func enableCors(w *http.ResponseWriter) {
	(*w).Header().Set("Access-Control-Allow-Origin", "*")
}

func monitorSite(w http.ResponseWriter, r *http.Request) {
	// Obter a URL da query string
	enableCors(&w)
	url := r.URL.Query().Get("url")
	if url == "" {
		http.Error(w, "URL não fornecida", http.StatusBadRequest)
		return
	}

	fmt.Println("Monitorando URL:", url)
	// Fazer a requisição GET
	client := http.Client{Timeout: 5 * time.Second}
	resp, err := client.Get(url)
	if err != nil {
		fmt.Fprintf(w, "Erro ao acessar %s: %v", url, err)
		return
	}
	defer resp.Body.Close()

	// Retornar o status
	fmt.Fprintf(w, "Status de %s: %s", url, resp.Status)
}

func main() {
	// Servir arquivos estáticos (HTML, CSS, JS)
	fs := http.FileServer(http.Dir("./static"))
	http.Handle("/static/", http.StripPrefix("/static", fs))

	// Servir o arquivo index.html na rota portfolio
	http.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
		http.ServeFile(w, r, "static/index.html")
	})
	http.HandleFunc("/monitor", func(w http.ResponseWriter, r *http.Request) {
		http.ServeFile(w, r, "static/monitor.html")
	})

	http.HandleFunc("/weather", func(w http.ResponseWriter, r *http.Request) {
		http.ServeFile(w, r, "static/weather.html")
	})

	http.HandleFunc("/monitoraction", monitorSite)

	fmt.Println("Iniciando o servidor na porta 8080")
	http.ListenAndServe(":8080", nil)
}
