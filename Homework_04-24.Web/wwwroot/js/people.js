$(() => {
    const modal = new bootstrap.Modal($('.modal')[0]);

    refreshTable();

    function refreshTable() {
        $('tbody').empty();
        $.get("/home/getpeople", function (people) {
            people.forEach(function (person) {
                $('tbody').append(`<tr>
                       <td>${person.firstName}</td>
                       <td>${person.lastName}</td>
                       <td>${person.age}</td>
                       <td><button class="btn btn-success" id="edit-person" data-person-id=${person.id}>Edit</button></td>
                       <td><button class="btn btn-danger" id="delete-person" data-person-id=${person.id}>Delete</button></td>
                       </tr>`)
            })
        })
    }

    $('#add-person').on('click', function () {

        $('#firstName').val('');
        $('#lastName').val('');
        $('#age').val('');

        $('#update-person').hide();
        $('#save-person').show();
        $('.modal-title').text("Add person");

        modal.show();
    })

    $('#save-person').on('click', function () {

        const firstName = $('#firstName').val();
        const lastName = $('#lastName').val();
        const age = $('#age').val();

        $.post("/home/addperson", { firstName, lastName, age }, function () {
            modal.hide();
            refreshTable();
        })
    })

    $('table').on('click', '#edit-person', function () {
        const button = $(this);
        const id = button.data('person-id');

        $.get("/home/getbyid", { id }, function ({ id, firstName, lastName, age }) {
            $('#firstName').val(firstName);
            $('#lastName').val(lastName);
            $('#age').val(age);
            modal.show();
        })

        $('.modal').data('person-id', id)
        $('#update-person').show();
        $('#save-person').hide();
        $('.modal-title').text("Edit person");

    })


    $('#update-person').on('click', function () {
        const id = $('.modal').data('person-id');
        const firstName = $('#firstName').val();
        const lastName = $('#lastName').val();
        const age = $('#age').val();

        $.post("/home/editperson", { id, firstName, lastName, age }, function () {
            modal.hide();
            refreshTable();
        })
    })

    $('table').on('click', '#delete-person', function () {
        const button = $(this);
        const id = button.data('person-id');

        $.post("home/deleteperson", { id }, function () {
            refreshTable();
        })

    })
})