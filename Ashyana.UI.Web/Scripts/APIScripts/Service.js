//app.service("APIService", function ($http) {
//    this.getSubs = function () {
//        var url = 'api/Floors';
//        return $http.get(url).then(function (response) {
//            return response.data;
//        });        
//    }
//    this.saveSubscriber = function (sub) {
//        return $http({
//            method: 'post',
//            data: sub,
//            url: 'api/Floors'
//        });
//    }
//    this.updateSubscriber = function (sub) {
//        return $http({
//            method: 'put',
//            data: sub,
//            url: 'api/Floors'
//        });
//    }
//    this.deleteSubscriber = function (subID) {
//        var url = 'api/Floors/' + subID;
//        return $http({
//            method: 'delete',
//            data: subID,
//            url: url
//        });
//    }
//});




app.service('APIFloorService', function ($http) {
    alert('service called')

    var urlGet = '';
    this.getAll = function (apiRoute) {
        urlGet = apiRoute;
        return $http.get(urlGet);
    }
});

