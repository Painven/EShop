export default {
    api_base: "https://localhost:7229/api",

    async SendRequest(requestUri, method, bodyData) {
        var settings = {
            method: method || "GET",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
            },
        };
        if (method != "GET" && bodyData) {
            settings.body = JSON.stringify(bodyData);
        }

        try {
            const uri = `${this.api_base}/${requestUri}`;
            console.log("Начинаем запрос по URI: " + uri);

            const fetchResponse = await fetch(uri, settings);
            const serverResponse = await fetchResponse.json();

            console.log("Ответ сервера: " + serverResponse);

            return serverResponse;
        } catch (ex) {
            console.log(ex);
        }

        return null;
    },
};
