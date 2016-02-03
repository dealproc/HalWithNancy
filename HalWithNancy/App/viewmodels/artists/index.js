﻿define(["durandal/system", "durandal/app", "plugins/router", "jquery", "knockout", "services/artistService"], function (system, app, router, $, ko, artistService) {
    var ctor = function () {
        var _this = this;

        _this.activate = function (params) {
            if (params !== undefined) {
                artistService.getPage(params.page, params.pageSize).done(_this.grid.bindPage);
            } else {
                artistService.getPage(1, ko.utils.unwrapObservable(_this.grid.pageSize)).done(_this.grid.bindPage);
            }
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
            edit: function(btn){
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

                router.navigate('?page=' + _this.grid.pageNumber() + '&pageSize=' + _this.grid.pageSize(), false);
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
            columns: [
                { header: '', property: 'hi', controls: 'buttons', css: 'col-sm-2 col-md-1' },
                { header: 'Artist Name', property: 'name', css: 'col-sm-10 col-md-11' }
            ]
        };

    };
    return ctor;
});