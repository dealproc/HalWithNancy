define(["durandal/system", "durandal/app", "plugins/router", "jquery", "knockout", "services/albumService"], function (system, app, router, $, ko, albumService) {
    var ctor = function () {
        var _this = this;

        _this.grid = {
            collectionName: 'albums',
            dataService: albumService,
            pageNumber: 1,
            pageSize: 10,
            keywords: '',
            sortBy: '',
            sortByDir: '',
            records: [],
            columns: [
                { header: '', property: 'hi', controls: 'buttons', css: 'col-sm-2 col-md-1', sort: ko.observable(undefined), canSort: false },
                { header: 'Artist Name', property: 'artist',       css: 'col-sm-5 col-md-5', sort: ko.observable(undefined), canSort: true },
                { header: 'Album Name', property: 'title',         css: 'col-sm-5 col-md-6', sort: ko.observable(undefined), canSort: true },
            ]
        };

        _this.activate = function (params) {
            if (params !== undefined) {
                _this.grid.pageNumber = params.page || 1;
                _this.grid.pageSize = params.pageSize || 10;
                _this.grid.keywords = params.keywords || '';
                _this.grid.sortBy = params.sortBy || '';
                _this.grid.sortByDir = params.sortByDir || '';
            };
        };
    };
    return ctor;
});