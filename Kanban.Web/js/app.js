angular.module('kanban', ['ngResource']);
angular.module('kanban').value('apiUrl', 'http://localhost:62602/api');

angular.module('kanban').controller('IndexController', function ($scope, $resource, apiUrl) {
    $scope.newList = {};
    $scope.newCard = {};

    var ListResource = $resource(apiUrl + '/lists/:listId', { listId: '@id' }, {
        'cards': {
            url: apiUrl + '/lists/:listId/cards',
            method: 'GET',
            isArray: true
        }
    });

    var CardResource = $resource(apiUrl + '/cards/:listId', { listId: '@id' }, {
        'cards': {
            url: apiUrl + '/lists/:listId/cards',
            method: 'POST',
            isArray: true
        }
    });

    $scope.addList = function () {
        ListResource.save($scope.newList, function ()
        {
            alert('save successful'); activate();
        });
    };

    $scope.addCard = function () {
        CardResource.save($scope.newCard, function () {
            alert('card added!'); activate();
        });
    };

    function activate() {
        ListResource.query(function (data) {
            $scope.lists = data;

            $scope.lists.forEach(function (list) {
                list.cards = ListResource.cards({ listId: list.ListID });
                list.newCard = {};
            });
        });
    }

    activate();

});