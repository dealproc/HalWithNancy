define(["durandal/system", "durandal/app", "plugins/router", "jquery", "knockout"], function (system, app, router, $, ko) {
    var columnDefaults = {
        header: '',
        property: '',
        controls: undefined,
        css: 'col-sm-1 col-md-1',
        sort: ko.observable(undefined),
        canSort: false
    };

    var normalizeColumns = function (columns) {
        var cols = ko.utils.unwrapObservable(columns);

        if (!(cols instanceof Array)) {
            throw new Error("Grid columns must be an array.");
        }

        return cols.map(function (column) {
            var normalized = undefined;
            if (typeof column == 'string' || column instanceof String) {
                normalized = $.extend({ property: column, header: column }, columnDefaults);
            } else if (column.property === undefined) {
                throw new Error("Columns must contain a property named 'property' so that they can look up their value");
            } else {
                normalized = $.extend({}, columnDefaults, column);
            }
            normalized.sortIcon = ko.pureComputed(function () {
                var state = ko.utils.unwrapObservable(this.sort);
                if (state === undefined) {
                    return "fa fa-fw fa-sort text-muted";
                } else if (state) {
                    return "fa fa-fw fa-sort-amount-asc";
                }
                return "fa fa-fw fa-sort-amount-desc";
            }, normalized);
            return normalized;
        });
    };

    var dgrid = function (config) {
        if (config.dataService === undefined) {
            throw new Error("Data service must be provided.");
        }
        var grid = this;

        grid.dataService = config.dataService;

        grid.columns = normalizeColumns(config.columns);

        if (config.sortBy !== undefined && config.sortByDir !== undefined) {
            var properties = config.sortBy.split(',');
            var directions = config.sortByDir.split(',');

            for (var i = 0; i < properties.length; i++) {
                var property = properties[i];
                var direction = directions[i];

                for (var idx = 0; idx < grid.columns.length; idx++) {
                    var col = grid.columns[idx];
                    if (col.property === property) {
                        col.sort(direction === 'asc');
                        idx = grid.columns.length;
                    }
                }
            }
        }

        grid.pageNumber = ko.isObservable(config.pageNumber) ? config.pageNumber : ko.observable(config.pageNumber || 1);
        grid.pageSize = ko.isObservable(config.pageSize) ? config.pageSize : ko.observable(config.pageSize || 10);
        grid.pageSizeOptions = ko.isObservable(config.pageSizeOptions) ? config.pageSizeOptions : ko.observable(config.pageSizeOptions || [10, 25, 50, 100]);
        grid.totalPages = ko.isObservable(config.totalPages) ? config.totalPages : ko.observable(config.totalPages || 0);
        grid.totalRecords = ko.isObservable(config.totalRecords) ? config.totalRecords : ko.observable(config.totalRecords || 0);
        grid.keywords = ko.isObservable(config.keywords) ? config.keywords : ko.observable(config.keywords || '');
        grid.keywords.extend({ rateLimit: { timeout: 500, method: "notifyWhenChangesStop" } }).subscribe(function () { grid.dataService.getPage(grid.parameters()).done(grid.bind); });
        grid.pager = ko.observableArray([]);
        grid.records = ko.isObservable(config.records) ? config.records : ko.observableArray(config.records || []);
        grid.parameters = function () {
            var parameters = {};

            if (ko.utils.unwrapObservable(grid.pageNumber) !== undefined) {
                parameters['page'] = ko.utils.unwrapObservable(this.pageNumber);
            }
            if (ko.utils.unwrapObservable(grid.pageSize) !== undefined) {
                parameters['pageSize'] = ko.utils.unwrapObservable(this.pageSize);
            }
            var keywords = ko.utils.unwrapObservable(this.keywords);
            if (keywords !== undefined && keywords !== '' && keywords !== null) {
                parameters['keywords'] = keywords;
            }

            var sortByCols = [];
            var sortByDir = [];

            for (var i = 0; i < grid.columns.length; i++) {
                var col = grid.columns[i];
                var canSort = ko.utils.unwrapObservable(col.canSort);
                var sort = ko.utils.unwrapObservable(col.sort);
                var property = ko.utils.unwrapObservable(col.property);

                if (canSort && (sort !== undefined && sort !== null)) {
                    sortByCols.push(property);
                    sortByDir.push(sort ? 'asc' : 'desc');
                }
            }

            if (sortByCols.length) {
                parameters['sortBy'] = sortByCols.toString();
                parameters['sortByDir'] = sortByDir.toString();
            }

            return parameters;
        };
        grid.getColumnText = function (column, row) {
            return row[column.property];
        };
        grid.colType = function (cell) {
            if (cell.col.controls !== undefined) {
                return cell.col.controls;
            }
            return 'text';
        };
        grid.setSort = function (col) {
            var state = ko.utils.unwrapObservable(col.sort);
            if (state === undefined) {
                col.sort(true);
            } else if (state) {
                col.sort(false);
            } else {
                col.sort(undefined);
            }

            grid.pageNumber(1);

            grid.dataService.getPage(grid.parameters()).done(grid.bind);
        };
        grid.onPageChange = function (btn) {
            grid.dataService.getUri(btn.href).done(grid.bind);
        };
        grid.bind = function (data) {
            grid.pageNumber(data.pageNumber);
            grid.totalPages(data.totalPages);
            grid.totalRecords(data.totalResults);

            var btns = [];

            btns.push($.extend({ 'enabled': true }, { title: "Previous" }, (data._links['prev'] !== undefined) ? data._links['prev'] : { 'enabled': false }));
            btns.push($.extend({ 'enabled': true }, { title: "Next" }, (data._links['next'] !== undefined) ? data._links['next'] : { 'enabled': false }));

            grid.pager([]);
            ko.utils.arrayPushAll(grid.pager, btns);

            grid.records([]);
            grid.records(data._embedded.artists);

            var instruction = router.activeInstruction();
            console.log(instruction);

            router.navigate(instruction.fragment + "?" + $.param(grid.parameters()), false);
        };

        grid.dataService.getPage(grid.parameters()).done(grid.bind);
    };

    return function widget() {
        var _widget = this;

        _widget.activate = function (config) {
            $.extend(_widget, new dgrid(config));
        }
    };
});