document.addEventListener("DOMContentLoaded", function() {
    const monitorBtn = document.getElementById("monitor-btn");
    const urlInput = document.getElementById("url");
    const result = document.getElementById("monitor-result");

    if (monitorBtn) {
        monitorBtn.addEventListener("click", function() {
            let url = urlInput.value.trim();

            // Verificar se a URL começa com "http://" ou "https://"
            if (!url.startsWith("http://") && !url.startsWith("https://")) {
                url = "https://" + url; // Adicionar "https://" automaticamente
            }

            // Fazer a requisição para o backend
            fetch(`/monitoraction?url=${encodeURIComponent(url)}`)
                .then(response => response.text())
                .then(data => {
                    result.textContent = data;
                })
                .catch(error => {
                    result.textContent = `Erro: ${error}`;
                });
        });
    }
});
