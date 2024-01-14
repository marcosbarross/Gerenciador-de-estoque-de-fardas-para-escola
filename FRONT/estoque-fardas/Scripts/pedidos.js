$(document).ready(function () {
    function fetchAndPopulateTable() {
        $.ajax({
            url: 'https://localhost:7179/GetPedidos',
            type: 'GET',
            success: function (data) {
                populateTable(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
    }


    function populateTable(data) {
        var tableBody = $('#itemTable tbody');
        tableBody.empty(); // Clear existing rows

        data.forEach(function (item) {
            var row = '<tr>' +
                '<td><input type="text" class="form-control" value="' + item.id + '" readonly></td>' +
                '<td><input type="text" class="form-control" value="' + item.nomeCliente + '" readonly></td>' +
                '<td><input type="text" class="form-control" value="' + item.produtoNome + '" readonly></td>' +
                '<td><input type="text" class="form-control" value="' + item.produtoTamanho + '" ></td>' +
                '<td><input type="text" class="form-control" value="' + item.quantidade + '" ></td>' +
                '<td><button class="btn btn-danger btn-remove" data-rowid="' + item.id + '">Remover</button></td>' +
            '</tr>';
        
            tableBody.append(row);
        });
        
    $('.btn-remove').click(function () {
        var rowId = $(this).closest('tr').find('input[type="text"]').first().val();

        $.ajax({
            url: 'https://localhost:7179/DeletarPedidos',
            type: 'DELETE',
            contentType: 'application/json',
            data: JSON.stringify({ Id: rowId }),
            success: function (response) {
                console.log(response);
    
                $(this).closest('tr').remove();
                location.reload();
            },
            error: function (error) {
                console.error('Error deleting item:', error);
            }
        });
    });
   
    }




    // Call the function to fetch and populate the table on page load
    fetchAndPopulateTable();

    // Optional: Add an event listener to refresh the table when a button is clicked
    $('#finalizarBtn').click(function () {
        fetchAndPopulateTable();
    });
});