app.controller("UserList", ["$scope", "UserListFactory", "$timeout", "$location", "$window", function ($scope, UserListFactory, $timeout, $location, $window) {


    $scope.save = function (model) {
        debugger;
        UserListFactory.save(model).then(function (success) {
            alert(success.data.response_msg);
            //$window.location.reload();
        });
    };
    //$scope.$watch("Role_ID", function (newVal, oldVal) {
    //    debugger;
    //    if (typeof newVal == "undefined" || newVal == 0)
    //        return;
    //    if (newVal == oldVal)
    //        return;

    //    $scope.usergridData2 = $scope.usergridData;

    //    $scope.usergridData = [];
    //    $('#table_id').dataTable().fnDestroy();
    //    $scope.usergridData = $scope.usergridData2.filter(function (el) {
    //        return el.role_id == newVal
    //    });

    //});

    UserListFactory.init(
        function (success) {
            $scope.usergridData = success[0].data;
            $scope.getroles = success[1].data;

            $timeout(() => {
                var data = $('#table_id').DataTable({
                    lengthMenu: [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                    bDestroy: true
                }).row(this).data();
            })
        });
}]);

app.factory("UserListFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        $q.all([
            this.getUserGrid(),
            CommonFactory.getRoles()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getUserGrid = function () {
        return $http.post("/User/GetUserGrid");
    },
        this.save = function (model) {
            return $http.post(("/User/Save"), { model: model });
        }
    return this;
}]);


app.controller("User", ["$scope", "UserFactory", "$timeout", "$location", "$window", "$validator", function ($scope, UserFactory, $timeout, $location, $window, $validator) {

    $scope.emailFormat = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;

    $scope.save = function () {
        debugger;
        $validator.validate($scope).success(function () {
            UserFactory.save($scope.model).then(function (success) {
                alert(success.data);
                window.location.href = window.location.origin + '/User';
                //$window.location.reload();
            });
        });
    };


    UserFactory.init(
        function (success) {
            debugger;
            $scope.model = success[0].data;
            $scope.getroles = success[1].data;
        });
}]);
app.factory("UserFactory", ["$rootScope", "$http", "$q", "CommonFactory", function ($rootScope, $http, $q, CommonFactory) {
    this.init = function (success, failure) {
        var id = 0;
        var urls = window.location.href.split('?')[0].split('/');
        if (!isNaN(urls[urls.length - 1]))
            id = eval(urls[urls.length - 1]);

        $q.all([
            this.getItem(id),
            CommonFactory.getRoles()
        ]).then(function (msg) {
            success(msg);
        }, failure);
    }
    this.getItem = function (id) {
        return $http.post(("/User/GetItem"), { id: id })
    },
        this.save = function (model) {
            return $http.post(("/User/Save"), { model: model });
        }
    this.getGridDetail = function (AsOnDate, ClaimType) {
        return $http.get('/User/GetGridDetail/');
    }
    this.getClaimType = function () {
        return $http.get('/User/GetDetails/');
    }
    return this;
}]);