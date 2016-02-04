requirejs.config({
    paths: {
        "text": "../Scripts/text",
        "durandal": "../Scripts/durandal",
        "plugins": "../Scripts/durandal/plugins",
        "scripts": "../Scripts"
    }
});

define("jquery", function () { return jQuery; });
define("knockout", ko);

ko.bindingHandlers.foreachprop = {
    transformObject: function (obj) {
        var properties = [];
        for (var key in obj) {
            if (obj.hasOwnProperty(key)) {
                properties.push({ key: key, value: obj[key] });
            }
        }
        return properties;
    },
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            properties = ko.bindingHandlers.foreachprop.transformObject(value);
        ko.applyBindingsToNode(element, { foreach: properties }, bindingContext);
        return { controlsDescendantBindings: true };
    }
};

define(["durandal/system", "durandal/app", "durandal/viewLocator"], function (system, app, viewLocator) {
    //>>excludeStart("build",true);
    system.debug(true);
    //>>excludeEnd("build");

    app.title = "HAL+Json Demo";
    app.configurePlugins({
        router: true,
        dialog: true,
        widget: {
            kinds: ['dgrid']
        }
    });

    app
        .start()
        .then(function () {
            viewLocator.useConvention();
            app.setRoot("shell");
        });
});