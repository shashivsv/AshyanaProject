
app.controller('APIFloorController', ['$scope', 'APIFloorService',

function ($scope, APIFloorService) {
    alert('controller called')
    // this is base url 
    var baseUrl = '/api/Floors/';
    // get all student from databse
    $scope.getFloors = function () {
        var apiRoute = baseUrl + 'GET';
        var _student = APIFloorService.getAll(apiRoute);
        _student.then(function (response) {
            $scope.floors = response.data;
        },
        function (error) {
            console.log("Error: " + error);
        });

    }
    $scope.getFloors();

}]);

//getAll();
//function getAll() {
//    var servCall = APIService.getSubs();
//    servCall.then(function (d) {
//        $scope.subscriber = d;
//    }, function (error) {
//        console.log('Oops! Something went wrong while fetching the data.')
//    });
//}
//    $scope.saveSubs = function () {
//        var sub = {
//            MailID: $scope.mailid,
//            SubscribedDate: new Date()
//        };
//        var saveSubs = APIService.saveSubscriber(sub);
//        saveSubs.then(function (d) {
//            getAll();
//        }, function (error) {
//            console.log('Oops! Something went wrong while saving the data.')
//        })
//    };
//    $scope.makeEditable = function (obj) {
//        obj.target.setAttribute("contenteditable", true);
//        obj.target.focus();
//    };
//    $scope.updSubscriber = function (sub, eve) {
//        sub.MailID = eve.currentTarget.innerText;
//        var upd = APIService.updateSubscriber(sub);
//        upd.then(function (d) {
//            getAll();
//        }, function (error) {
//            console.log('Oops! Something went wrong while updating the data.')
//        })
//    };
//    $scope.dltSubscriber = function (subID) {
//        var dlt = APIService.deleteSubscriber(subID);
//        dlt.then(function (d) {
//            getAll();
//        }, function (error) {
//            console.log('Oops! Something went wrong while deleting the data.')
//        })
//    };
//});