(function () {

    //fetch('', {
    //    method: 'POST',
    //    cache: 'no-cache',
    //    headers: {
    //        'Content-Type': 'application/json'
    //    }
    //})

    window.wrappedFetch = function (url, obj) {
        //return fetch(url, JSON.parse(obj)).then(r => {
        //    if (r.status !== 200) {
        //        console.log(r.status, r.statusText);
        //        throw new Error(r.statusText);
        //    }
        //    return r.json();
        //});
        return fetch(url, JSON.parse(obj)).then(async r => {
            return {
                status: r.status,
                json: await r.text()
            };
        });
    };

})();