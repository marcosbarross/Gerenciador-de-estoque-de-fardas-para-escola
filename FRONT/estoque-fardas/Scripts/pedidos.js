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
                '<td><button class="btn btn-success btn-editar" type="button" data-bs-toggle="tooltip" title="Editar" data-id="' + item.id + '">Editar</button></td>' +
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

        $('.btn-editar').click(function () {
            var rowId = $(this).data('id');
            var produtoTamanho = $(this).closest('tr').find('input[type="text"]').eq(3).val();
            var quantidade = $(this).closest('tr').find('input[type="text"]').eq(4).val();

            console.log('Row ID: ' + rowId);
            console.log('Produto Tamanho: ' + produtoTamanho);
            console.log('Quantidade: ' + quantidade);

            $.ajax({
                url: 'https://localhost:7179/EditarPedidos/',
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify({
                    Id: rowId,
                    produtoTamanho: produtoTamanho,
                    Quantidade: quantidade
                }),
                success: function (response) {
                    console.log(response);
                    fetchAndPopulateTable();
                },
                error: function (error) {
                    console.error('Error editing item:', error);
                }
            });
        });
    }

    fetchAndPopulateTable();
});
