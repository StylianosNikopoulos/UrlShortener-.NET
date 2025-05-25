document.addEventListener("DOMContentLoaded", function () {
    const shortenBtn = document.getElementById("shortenBtn");
    const urlInput = document.getElementById("longUrl");
    const resultDiv = document.getElementById("result");

    shortenBtn.addEventListener("click", async () => {
        const longUrl = urlInput.value.trim();

        if (!longUrl) {
            resultDiv.innerHTML = `<div class="alert alert-danger">Please enter a URL.</div>`;
            return;
        }

        shortenBtn.disabled = true;
        urlInput.disabled = true;
        resultDiv.innerHTML = `<p>Loading...</p>`;

        try {
            const response = await fetch("/Home/ShortenUrl", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ longUrl })
            });

            const data = await response.json();

            if (response.ok && data.shortUrl) {
                resultDiv.innerHTML = `
                    <div class="alert alert-success">
                        <strong>Shortened URL:</strong> 
                        <a href="${data.shortUrl}" target="_blank">${data.shortUrl}</a>
                    </div>`;
            }
            else
            {
                resultDiv.innerHTML = `<div class="alert alert-danger">Error: ${data?.error || "Unexpected response"}</div>`;
            }
        } catch (err) {
            console.error("Error during fetch:", err);
            resultDiv.innerHTML = `<div class="alert alert-danger">Unexpected error occurred.</div>`;
        } finally {
            shortenBtn.disabled = false;
            urlInput.disabled = false;
        }
    });
});
