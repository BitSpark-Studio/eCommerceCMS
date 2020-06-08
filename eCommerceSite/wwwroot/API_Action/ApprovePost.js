function Approve(uri) {

    swal({
        title: "Are you sure?",
        text: "Want to Approve",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willApprove) => {
        if (willApprove) {

            /* var value = {
                 id: $("#Delete").val(),
             };*/
            //// document.writeln(value.email);
            $.ajax({
                url: '/api/Admin/' + uri,
                //headers: { 'ApiKey': token },
                headers: { 'ApiKey': 'secret' },
                type: 'PUT',
                dataType: 'json',
                /// data: value,
                success: function (data, textStatus, xhr) {

                    swal({
                        icon: 'success',
                        title: 'Approved',
                        text: 'Post Approved Successfully',
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
