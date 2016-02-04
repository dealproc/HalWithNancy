define(["durandal/system", "durandal/app", "jquery"], function (system, app, $) {
    return {
        getUri: function (uri) {
            return system.defer(function (dfd) {
                $.ajax({
                    headers: {
                        "Accept": "application/hal+json",
                    },
                    dataType: "json",
                    cache: false,
                    url: uri
                })
                .done(function (pagedList) {
                    dfd.resolve(pagedList);
                });
            }).promise();
        },
        getPage: function (queryString) {
            return system.defer(function (dfd) {
                $.ajax({
                    headers: {
                        "Accept": "application/hal+json",
                    },
                    data: $.param(queryString),
                    dataType: "json",
                    cache: false,
                    url: "/artists"
                })
                .done(function (pagedList) {
                    dfd.resolve(pagedList);
                });
            }).promise();
        }
    }
});