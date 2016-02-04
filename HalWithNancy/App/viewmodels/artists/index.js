define(["durandal/system", "durandal/app", "plugins/router", "jquery", "knockout", "services/artistService"], function (system, app, router, $, ko, artistService) {
    var ctor = function () {
        var _this = this;

        _this.activate = function (params) {

            if (params !== undefined) {
                _this.grid.pageNumber(params.page || 1);
                _this.grid.pageSize(params.pageSize || 10);
                _this.grid.keywords(params.keywords || '');

                if (params.sortBy !== undefined && params.sortByDir !== undefined) {
                    var properties = params.sortBy.split(',');
                    var directions = params.sortByDir.split(',');

                    for (var i = 0; i < properties.length; i++) {
                        var property = properties[i];
                        var direction = directions[i];

                        for (var idx = 0; idx < _this.grid.columns.length; idx++) {
                            var col = _this.grid.columns[idx];
                            if (col.property === property) {
                                col.sort(direction === 'asc');
                                idx = _this.grid.columns.length;
                            }
                        }
                    }
                }
            };

            artistService.getPage(_this.grid.gridParameters()).done(_this.grid.bindPage);
        };

        _this.grid = {
            data: ko.observableArray([]),
            dataService: artistService,
            pageSize: ko.observable(10),
            pageSizeOptions: ko.observableArray([10, 25, 50, 100]),
            pageNumber: ko.observable(1),
            totalPages: ko.observable(0),
            totalRecords: ko.observable(0),
            pager: ko.observableArray([]),
            keywords: ko.observable(''),
            gridParameters: function () {
                var parameters = {};

                if (ko.utils.unwrapObservable(this.pageNumber) !== undefined) {
                    parameters['page'] = ko.utils.unwrapObservable(this.pageNumber);
                }
                if (ko.utils.unwrapObservable(this.pageSize) !== undefined) {
                    parameters['pageSize'] = ko.utils.unwrapObservable(this.pageSize);
                }
                var keywords = ko.utils.unwrapObservable(this.keywords);
                if (keywords !== undefined && keywords !== '' && keywords !== null) {
                    parameters['keywords'] = keywords;
                }

                var sortByCols = [];
                var sortByDir = [];

                for (var i = 0; i < this.columns.length; i++) {
                    var col = this.columns[i];
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
            },
            edit: function (btn) {
                alert(btn.href);
            },
            loadPage: function (btn) {
                _this.grid.dataService.getUri(btn.href).done(_this.grid.bindPage);
            },
            bindPage: function (data) {
                _this.grid.pageNumber(data.pageNumber);
                _this.grid.totalPages(data.totalPages);
                _this.grid.totalRecords(data.totalResults);

                var btns = [];

                btns.push($.extend({ 'enabled': true }, { title: "Previous" }, (data._links['prev'] !== undefined) ? data._links['prev'] : { 'enabled': false }));
                btns.push($.extend({ 'enabled': true }, { title: "Next" }, (data._links['next'] !== undefined) ? data._links['next'] : { 'enabled': false }));

                _this.grid.pager([]);
                ko.utils.arrayPushAll(_this.grid.pager, btns);

                _this.grid.data([]);
                _this.grid.data(data._embedded.artists);

                router.navigate("?" + $.param(_this.grid.gridParameters()), false);
            },
            colType: function (cell) {
                if (cell.col.controls !== undefined) {
                    return cell.col.controls;
                }
                return 'text';
            },
            getColumnText: function ($colInfo, $row) {
                return $row[$colInfo.property];
            },
            setSort: function (col) {
                var state = ko.utils.unwrapObservable(col.sort);
                if (state === undefined) {
                    col.sort(true);
                } else if (state) {
                    col.sort(false);
                } else {
                    col.sort(undefined);
                }

                _this.grid.pageNumber(1);

                artistService.getPage(_this.grid.gridParameters()).done(_this.grid.bindPage);
            },
            columns: [
                { header: '', property: 'hi', controls: 'buttons', css: 'col-sm-2 col-md-1', sort: ko.observable(undefined), canSort: false },
                { header: 'Artist Name', property: 'name', css: 'col-sm-10 col-md-11', sort: ko.observable(undefined), canSort: true }
            ]
        };

        for (var i = 0; i < _this.grid.columns.length; i++) {
            var col = _this.grid.columns[i];
            col.sortIcon = ko.pureComputed(function () {
                var state = ko.utils.unwrapObservable(this.sort);
                if (state === undefined) {
                    return "fa fa-fw fa-sort text-muted";
                } else if (state) {
                    return "fa fa-fw fa-sort-amount-asc";
                }
                return "fa fa-fw fa-sort-amount-desc";
            }, col);
        }
    };
    return ctor;
});