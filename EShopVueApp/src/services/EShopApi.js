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
            const fetchResponse = await fetch(
                `${this.api_base}/${requestUri}`,
                settings
            );
            const serverResponse = await fetchResponse.json();
            return serverResponse;
        } catch (ex) {
            console.log(ex);
        }

        return null;
    },
};
