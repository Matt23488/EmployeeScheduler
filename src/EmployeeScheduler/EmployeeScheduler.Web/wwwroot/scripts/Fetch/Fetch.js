(function () {

    const fetchObj = {};

    fetchObj.fetch = async function (url, obj) {
        const r = await fetch(url, obj);
        return {
            status: r.status,
            data: await r.json()
        };
    };

    window.blazorFetchService = fetchObj;

})();