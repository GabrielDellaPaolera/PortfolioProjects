document.addEventListener("DOMContentLoaded", function() {

    // responsável pela parte do monitoramento 
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

 // responsável pela parte da previsão do tempo
    const weatherForm = document.getElementById("weather-form");
    const cityInput = document.getElementById("city");
    const weatherResult = document.getElementById("weather-result");

    if (weatherForm) {
        weatherForm.addEventListener("submit", function(event) {
            event.preventDefault();
            const city = cityInput.value.trim();

            fetch(`http://localhost:5000/WeatherForecast/${city}`)
                
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            
                .then(data => {
                    weatherResult.style.display = "block";
                    weatherResult.textContent = JSON.stringify(data, null, 2);
                })
                .catch(error => {
                    weatherResult.style.display = "block";
                    weatherResult.textContent = `Erro: ${error}`;
                });
        });
    }
});
