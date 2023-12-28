$(document).ready(function () {
    var dataTable = $('#itemTable').DataTable({
        columns: [
            { data: 'nome' },
            {
                data: 'tamanho',
                /*render: function (data, type, row) {
                    var tamanhoOptions = [2, 4, 6, 8, 10, 12, 14, 16];
                    var dropdown = '<select class="form-select" data-rowid="' + row.id + '">';

                    for (var i = 0; i < tamanhoOptions.length; i++) {
                        dropdown += '<option value="' + tamanhoOptions[i] + '"';
                        if (data == tamanhoOptions[i]) {
                            dropdown += ' selected';
                        }
                        dropdown += '>' + tamanhoOptions[i] + '</option>';
                    }

                    dropdown += '</select>';
                    return dropdown;
                }*/},
            { data: 'preco' },
            { data: 'quantidadeRestante' },
            {
                data: 'quantidade',
                render: function (data, type, row) {
                    return '<div class="qty-box">' +
                        '<div class="input-group">' +
                        '<span class="input-group-prepend">' +
                        '<button class="btn quantity-left-minus" type="button" data-type="minus" data-rowid="' + row.nome + row.tamanho + '">-</button>' +
                        '</span>' +
                        '<input class="form-control input-number" type="text" name="quantity" value="0" data-rowid="' + row.nome + row.tamanho + '">' +
                        '<span class="input-group-prepend">' +
                        '<button class="btn quantity-right-plus" type="button" data-type="plus" data-rowid="' + row.nome + row.tamanho + '">+</button>' +
                        '</span>' 
                        '</div>' +
                        '</div>';
                }
            },
        ]
    });

    // Adiciona manipuladores de eventos para os botões de contagem
    $(document).on('click', '.quantity-left-minus, .quantity-right-plus', function () {
        var inputField = $(this).closest('.qty-box').find('.input-number');
        var currentValue = parseInt(inputField.val(), 10);

        if ($(this).hasClass('quantity-right-plus')) {
            inputField.val(currentValue + 1);
        } else if ($(this).hasClass('quantity-left-minus')) {
            // Permitir reduzir até 0
            inputField.val(Math.max(0, currentValue - 1));
        }
    });

    $('#finalizarBtn').click(function () {
        var table = $('#itemTable').DataTable();
        var data = table.rows().data();
    
        // Itera sobre os dados da tabela
        data.each(function (item) {
            var nome = item.nome;
            var tamanho = item.tamanho;
            var quantidadeInput = $('.input-number[data-rowid="' + nome + tamanho + '"]');
            var quantidadeValue = parseInt(quantidadeInput.val(), 10);
    
            // Verifica se a quantidade é maior que zero antes de chamar a API
            if (quantidadeValue > 0) {
                // Chama a API apenas se a quantidade for maior que zero
                $.ajax({
                    url: 'https://localhost:7179/ComprarItem',
                    type: 'PUT',
                    contentType: 'application/json',
                    data: JSON.stringify({
                        nome: nome,
                        tamanho: tamanho,
                        quantidadeComprada: quantidadeValue
                    }),
                    success: function (response) {
                        console.log('Compra realizada com sucesso para o item com nome ' + nome + ' e ' +  tamanho);
                        location.reload();
                    },
                    error: function () {
                        console.error('Erro ao realizar a compra para o item com nome ' + nome);
                    } 
                });
            } else {
                console.log('A quantidade para o item com nome ' + nome + ' é zero. A compra não será realizada.');
            }
        });
    });
    

    $.ajax({
        url: 'https://localhost:7179/ListarItens',
        type: 'GET',
        success: function (data) {
            dataTable.rows.add(data).draw();
        },
        error: function () {
            console.error('Erro ao obter a lista de itens.');
        }
    });
});
