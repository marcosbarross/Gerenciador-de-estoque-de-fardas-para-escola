$(document).ready(function () {
    console.log('Documento carregado');
    $('#AddButton').click(function () {
    console.log('Botão de adicionar clicado');
      // Obtém os valores do formulário
      var nomeProduto = $('input[placeholder="Nome do produto*"]').val();
      var tamanhoProduto = $('select.form-select').val();
      var precoProduto = $('input[placeholder="Preço"]').val();
      var quantidadeProduto = $('input[placeholder="Quantidade"]').val();

      // Cria o objeto JSON com os dados do formulário
      var jsonData = {
        "nome": nomeProduto,
        "tamanho": parseInt(tamanhoProduto),
        "preco": parseFloat(precoProduto),
        "quantidadeRestante": parseInt(quantidadeProduto)
      };

      // Faz a requisição POST para a API
      $.ajax({
        type: 'POST',
        url: 'https://localhost:7179/AddItem',
        contentType: 'application/json',
        data: JSON.stringify(jsonData),
        success: function (data) {
          // Manipule a resposta da API aqui, se necessário
          console.log('Requisição POST bem-sucedida:', data);
        },
        error: function (error) {
          // Manipule o erro da API aqui, se necessário
          console.error('Erro na requisição POST:', error);
        }
      });
    });
  });