

    function Renew(uri) {
        swal({
            title: "Are you sure?",
            text: "Want to Renew This User",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((willRenew) => {
            if (willRenew) {

                /* var value = {
                     id: $("#Delete").val(),
                 };*/
                //// document.writeln(value.email);
                $.ajax({
                    url: '/api/RenewUser/' + uri,
                    //headers: { 'ApiKey': token },
                    headers: { 'ApiKey': 'secret' },
                    type: 'PUT',
                    dataType: 'json',
                    /// data: value,
                    success: function (data, textStatus, xhr) {

                        swal({
                            icon: 'success',
                            title: 'Renewed',
                            text: 'User Renewed Successfully',
                        });

                        //// location.reload();
                        ///dataTable.ajax.reload();
                    },
                    error: function (xhr, textStatus, errorThrown) {

                        swal({
                            icon: 'error',
                            title: 'Ooopsss!',
                            text: 'Something Went Wrong',
                        });

                    }
                });
            }

        });
}

 