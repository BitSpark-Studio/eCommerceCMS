

function Product(uri) {

    /* var value = {
         id: $("#Delete").val(),
     };*/
    //// document.writeln(value.email);
    $.ajax({
        url: '/api/ProductDetails/' + uri,
        //headers: { 'ApiKey': token },
        headers: { 'ApiKey': 'secret' },
        type: 'GET',
        dataType: 'json',
        /// data: value,
        success: function (data, textStatus, xhr) {

            document.getElementById('exampleModalLongTitle').innerHTML = (data.value[0]);
            document.getElementById('Details').innerHTML = (data.value[1]);
            document.getElementById('img1').src = "/Product/" + data.value[2];
            document.getElementById('img2').src = "/Product/" + data.value[3];
            document.getElementById('img3').src = "/Product/" + data.value[4];
            document.getElementById('Name').innerHTML = (data.value[5]);
            document.getElementById('Phone').innerHTML = (data.value[6]);
            document.getElementById('Email').innerHTML = (data.value[7]);
            document.getElementById('dp').src = "/ProfilePicture/" + data.value[8];
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

