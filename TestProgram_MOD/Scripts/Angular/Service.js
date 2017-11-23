app.service("myService", function ($http) {

    //get All Eployee
    this.getPeople = function () {
        return $http.get("Home/GetAllPeople");
    };

    this.GetPerson = function (PersonID) {
        var response = $http({
            method: "post",
            url: "Home/getEmployeeByNo",
            params: {
                id: JSON.stringify(PersonID) //??
            }
        });
        return response;
    }

    this.AddPerson = function (person) {
        var response = $http({
            method: "post",
            url: "Home/AddPerson",
            data: JSON.stringify(person),
            dataType: "json"
        });
        return response;
    }

    this.UpdatePerson = function (personID) {
        var response = $http({
            method: "post",
            url: "Home/UpdatePerson",
            data: JSON.stringify(personID),
            dataType: "json"
        });
        return response;
    }

    this.DeletePerson = function (personID) {
        var response = $http({
            method: "post",
            url: "Home/DeletePerson",
            params: {
                id: JSON.stringify(personID)
            }
        });
        return response;
    }

});

