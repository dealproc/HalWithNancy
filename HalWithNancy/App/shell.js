define(["durandal/system", "durandal/app", "plugins/router"], function (system, app, router) {
    return {
        router: router,
        activate: function () {
            router
                .map([
                    { route: ['', 'home'], moduleId: 'viewmodels/artists/index', title: 'Artists', nav: true },
                    { route: 'artists', moduleId: 'viewmodels/artistgrid/index', title: 'Artists (reusable grid)', nav: true },
                    { route: 'artists/:id', moduleId: 'viewmodels/artists/dashboard', title: 'Artists', nav: false },
                ])
                .buildNavigationModel();
            return router.activate();
        }
    };
});