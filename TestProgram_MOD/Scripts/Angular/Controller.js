app.controller("myCntrl", function ($scope, myService) {
    $scope.TEXT = "asdasd";
    GetAllPeople(); //Show all people first

    //Get all records
    function GetAllPeople() {
        var getData = myService.getPeople();
        getData.then(function (p) {
            $scope.people = p.data;
        }, function () {
            alert('Error in getting records');
        });
    };

    //Bind fields with selected person
    $scope.EditPerson = function (person) {
        var getData = myService.GetPerson(person.ID);
        var selectedDate = new Date(parseInt(person.BornDate.substr(6)));
        getData.then(function (per) {
            $scope.selectedPerson = per.data;
            $scope.personId = person.ID;
            $scope.personLastName = person.LastName;
            $scope.personFirstName = person.FirstName;
            $scope.personBornDate = ConvertDateFromJson(person.BornDate);
            $scope.Action = "Update";
            $scope.divEmployee = true;
        },
            function () {
                alert('Error in getting records');
            });
    }

    //Add new people or update people
    $scope.AddUpdateEmployee = function () {

        var People = {
            LastName: $scope.personLastName,
            FirstName: $scope.personFirstName,
            BornDate: $scope.personBornDate
        };

        var getAction = $scope.Action;

        if (getAction === "Update") {
            //Update person
            People.Id = $scope.personId;
            var getData = myService.UpdatePerson(People);
            getData.then(function (msg) {
                GetAllPeople();
                ClearFields();
                closeNav();
                $scope.Action = "";
                //$scope.divEmployee = false;
            }, function () {
                alert('Error in updating record');
            });
        }
        else if (getAction === "Insert") {
            //Add person
            var getData = myService.AddPerson(People);
            getData.then(function (msg) {
                //$scope.divEmployee = false;
                GetAllPeople();
                ClearFields();
                closeNav();
                $scope.Action = "";
            }, function () {
                alert('Error in adding record');
            });
        }
    }

    //Delete selected person
    $scope.DeletePerson = function (person) {
        var getData = myService.DeletePerson(person.ID);
        getData.then(function (msg) {
            GetAllPeople();
            ClearFields();
            $scope.Action = "";
        }, function () {
            alert('Error in Deleting Record');
        });
    }

    //Set Action to Insert
    $scope.AddPerson = function () {
        $scope.Action = "Insert";
        ClearFields();
    }


    function ClearFields() {
        $scope.personId = "";
        $scope.personLastName = "";
        $scope.personFirstName = "";
        $scope.personBornDate = "";
    }

    function ConvertDateFromJson(setJsonDate) {
        var d = new Date(parseInt(setJsonDate.substr(6)));

        var year = d.getFullYear();
        var month = d.getMonth()+1;
        var date = d.getDate();

        if (month < 10) {
            month = "0" + month;
        }

        if (date < 10) {
            date = "0" + date;
        }
        return year + "." + month + "." + date;
    }

});