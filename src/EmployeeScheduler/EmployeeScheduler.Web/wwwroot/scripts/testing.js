(function () {

    //fetch('', {
    //    method: 'POST',
    //    cache: 'no-cache',
    //    headers: {
    //        'Content-Type': 'application/json'
    //    }
    //})

    window.wrappedFetch = function (url, obj) {
        return fetch(url, JSON.parse(obj)).then(r => r.json());
    };

})();