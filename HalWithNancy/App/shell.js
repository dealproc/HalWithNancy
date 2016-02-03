define(["durandal/system", "durandal/app", "plugins/router"], function (system, app, router) {
    return {
        router: router,
        activate: function () {
            router
                .map([
                    { route: 'artists/:id', moduleId: 'viewmodels/artists/dashboard', title: 'Artists', nav: false },
                    { route: ['', 'home'], moduleId: 'viewmodels/artists/index', title: 'Artists', nav: true }
                ])
                .buildNavigationModel();
            return router.activate();
        }
    };
});