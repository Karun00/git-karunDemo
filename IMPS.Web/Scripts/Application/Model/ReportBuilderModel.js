(function (ipmsRoot) {
    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    })

    var ReportBuilderModel = function (data) {

        var self = this;
        self.ReportbuilderId = ko.observable(data ? data.ReportbuilderId : "");
        self.ReportCategory = ko.observable(data ? data.ReportCategory : "");
        self.ReportObjectName = ko.observable(data ? data.ReportObjectName : "");
        self.ReportObjectType = ko.observable(data ? data.ReportObjectType : "");
        self.RecordStatus = ko.observable(data ? data.RecordStatus : "");
        self.OPERATOR_VALUE1 = ko.observable("");
        self.SelectedddlColumn = ko.observable("");

        self.exp = ko.observableArray([]);
        self.OrderBy = ko.observable(data ? data.OrderBy : "");
        self.Value = ko.observable(data ? data.Value : "");
        self.OrderByAD = ko.observable(data ? data.OrderByAD : "");
        self.allItems = ko.observableArray(data ? data.allItems : []);
        self.selectedItems = ko.observableArray(data ? data.selectedItems : []);

        self.reslt = ko.observable();
        //Report Builder Query Grid Field

        self.SearchFilter = ko.observable(data ? data.SearchFilter : "");
        self.DisplayColumns = ko.observable(data ? data.DisplayColumns : "");
        self.Parameters = ko.observable(data ? data.Parameters : "");
        self.Result = ko.observableArray(data ? data.Result : []);

        self.QueryTemplateName = ko.observable("").extend({ required: { message: '* Please Enter Query Template Name' } });
        self.ReportHeader = ko.observable("").extend({ required: { message: '* Please Enter Report Header' } });
        //ReportCategoryColumnTypes
        self.COLUMN_NAME = ko.observable("");

        self.DATA_TYPE = ko.observable("");

        self.Selectedmultilistcolumn = ko.observableArray([]);

        self.FromDate = ko.observable("");
        self.ToDate = ko.observable("");

        self.ValueNumber = ko.observable("");
         self.ValueText = ko.observable("");
         self.kendosuggest = ko.observable(true);

        self.DateValue = ko.observable("");
        self.SearchText = ko.observable("");
        self.ColumnName = ko.observable();
        self.query = ko.observable("");
        self.Text = ko.observable("");

        self.ResultValue = ko.observable("");

        self.ColumnTypes = ko.observable(data ? data.ColumnTypes : "");

        self.ColumnItems = ko.observableArray([]);

        self.QuerytemplateNameCopy = ko.observable("");

        self.QueryTemplateId = ko.observable();

        self.Flag = ko.observable();
        //////vm////////////////
    
        self.items = ko.observableArray([]);
        self.filters = ko.observable({});
        self.filteredItems = ko.computed(function () {
            var filters = self.filters();

            var items = ko.utils.arrayFilter(self.items(), function (item) {
                for (var col in filters) {
                    var v = (item[col] || '').toString();
                    var f = filters[col];
                    if (v.indexOf(f) < 0) return false;
                }
                return true;
            });
            return items;
        });
        var subscriptions = [];
        self.columnNames = ko.computed(function () {
            ko.utils.arrayForEach(subscriptions, function (s) { s.dispose(); });
            subscriptions = [];
            if (self.items().length === 0) return [];
            var props = [];
            var obj = self.items()[0];
            for (var name in obj) {
                var p = { name: name, filter: ko.observable('') };
                subscriptions.push(p.filter.subscribe(filterOnChanged, p));
                props.push(p);
            }
            return props;
        });

        var filterOnChanged = function (value) {
            console.log([this.name, value]);
            var filters = self.filters();
            filters[this.name] = value;
            self.filters(filters);
        };


        //////////paging //////////////////////

        self.currentPage = ko.observable();
        self.pageSize = ko.observable(10);
        self.currentPageIndex = ko.observable(0);
        self.maxPageIndex = ko.observable();
      
        self.pageindex = ko.observableArray([]);


    
        self.currentPage = ko.computed(function () {
            var pagesize = parseInt(self.pageSize(), 10),
            startIndex = pagesize * self.currentPageIndex(),
            endIndex = startIndex + pagesize;           
            return self.items.slice(startIndex, endIndex);
        });

        self.dataLength = ko.observable();

        self.maxPageIndex = ko.computed(function () {       
            return Math.ceil(ko.utils.unwrapObservable(parseInt(self.dataLength())) / 10)-1;
        }, this);
       
        self.nextPage = function () {
          
            if (((self.currentPageIndex() + 1) * self.pageSize()) < self.items().length) {
                self.currentPageIndex(self.currentPageIndex() + 1);
            }
            else {
               // self.currentPageIndex(0);
            }
        }

        self.LastnextPage = function () {          
            self.currentPageIndex((Math.ceil(self.items().length / self.pageSize())) - 1);           
        }

        self.previousPage = function () {
            if (self.currentPageIndex() > 0) {
                self.currentPageIndex(self.currentPageIndex() - 1);
            }
            else {
              //  self.currentPageIndex((Math.ceil(self.items().length / self.pageSize())) - 1);
            }
        }

        self.LastpreviousPage = function () {
            self.currentPageIndex(0);           
        }
         
        self.cache = function () { };
        self.set(data);
    }

    var ReportQueryTemplate = function (data) {
        var self = this;

        self.QueryTemplateId = ko.observable();
        self.QueryTemplateName = ko.observable();
        self.ReportHeader = ko.observable();
        self.UserQuery = ko.observableArray();

    }

    var ReportCategoryColumnTypeData = function (data) {
        var self = this;

        self.ReportCategoryColumnTypes = ko.observableArray(data ? $.map(data, function (item) { return new ReportCategoryColumnType(item); }) : []);
    }

    var ReportCategoryColumnType = function (data) {
        var self = this;

        self.COLUMN_NAME = ko.observable(data ? data.COLUMN_NAME : "");
        self.DATA_TYPE = ko.observable(data ? data.DATA_TYPE : "");
        self.query = ko.observable(data ? data.query : "");
    }

  


    ipmsRoot.ReportBuilderModel = ReportBuilderModel;
    ipmsRoot.ReportCategoryColumnTypeData = ReportCategoryColumnTypeData;
    ipmsRoot.ReportCategoryColumnType = ReportCategoryColumnType;
    ipmsRoot.ReportQueryTemplate = ReportQueryTemplate;

}(window.IPMSROOT));

IPMSROOT.ReportBuilderModel.prototype.set = function (data) {
    var self = this;
    self.ReportbuilderId(data ? (data.ReportbuilderId || "") : "");
    self.ReportCategory(data ? (data.ReportCategory || "") : "");
    self.ReportObjectName(data ? (data.ReportObjectName || "") : "");
    self.RecordStatus(data ? (data.RecordStatus || "") : "");
    self.OPERATOR_VALUE1(data ? (data.OPERATOR_VALUE1 || "") : "");
    self.SelectedddlColumn(data ? (data.SelectedddlColumn || "") : "");   
    self.Selectedmultilistcolumn(data ? (data.Selectedmultilistcolumn || "") : "");
    self.OrderBy(data ? (data.OrderBy || "") : "");
    self.Value(data ? (data.Value || "") : "");
    self.OrderByAD(data ? (data.OrderByAD || "") : "");
    self.allItems(data ? (data.allItems || []) : []);
    self.selectedItems(data ? (data.selectedItems || []) : []);
    self.SearchFilter(data ? (data.SearchFilter || "") : "");
    self.DisplayColumns(data ? (data.DisplayColumns || "") : "");
    self.Parameters(data ? (data.Parameters || "") : "");
    self.COLUMN_NAME(data ? (data.COLUMN_NAME || "") : "");
    self.DATA_TYPE(data ? (data.DATA_TYPE || "") : "");
    self.query(data ? (data.query || "") : "");
    self.QueryTemplateName(data ? (data.QueryTemplateName || "") : "");
    self.ColumnItems(data ? (data.ColumnItems || "") : "");

    self.cache.latestData = data;
}
IPMSROOT.ReportBuilderModel.prototype.reset = function () {
    this.set(this.cache.latestData);
}