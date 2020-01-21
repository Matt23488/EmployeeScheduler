(function () {

    const fetchObj = {};

    fetchObj.fetch = async function (url, obj) {
        const r = await fetch(url, obj);
        return {
            status: r.status,
            json: await r.text()
        };
    };

    window.blazorFetchService = fetchObj;

})();