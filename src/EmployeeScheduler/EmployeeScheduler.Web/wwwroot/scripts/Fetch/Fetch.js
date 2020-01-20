(function () {

    const fetchObj = {};

    fetchObj.fetch = function (url, obj) {
        //return fetch(url, JSON.parse(obj)).then(r => {
        //    if (r.status !== 200) {
        //        console.log(r.status, r.statusText);
        //        throw new Error(r.statusText);
        //    }
        //    return r.json();
        //});

        console.log(url, obj);
        return new Promise(resolve => {
            resolve({
                status: 401,
                json: JSON.stringify({
                    type: "type",
                    title: "title",
                    status: 401,
                    traceId: "???"
                })
            });
        });
        return fetch(url, JSON.parse(obj)).then(async r => {
            return {
                status: r.status,
                json: await r.text()
            };
        });
    };

    window.blazorFetchService = fetchObj;

})();