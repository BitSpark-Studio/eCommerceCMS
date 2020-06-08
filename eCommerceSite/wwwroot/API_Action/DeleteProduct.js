function Delete(uri) {

    swal({
        title: "Are you sure?",
        text: "Want to Delete",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {

            /* var value = {
                 id: $("#Delete").val(),
             };*/
            //// document.writeln(value.email);
            $.ajax({
                url: '/api/Owners/DeleteProduct/' + uri,
                //headers: { 'ApiKey': token },
                headers: { 'ApiKey': 'secret' },
                type: 'DELETE',
                dataType: 'json',
                /// data: value,
                success: function (data, textStatus, xhr) {

                    swal({
                        icon: 'success',
                        title: 'Deleted',
                        text: 'Cetagory Deleted Successfully',
                    });
                    $("#dataTable").load(location.href + " #dataTable");
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
