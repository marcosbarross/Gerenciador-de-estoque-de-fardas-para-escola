document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('form');

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        const nome = document.getElementById('nome').value;
        const aluno = document.getElementById('aluno').value;

        const data = {
            nome: nome,
            aluno: aluno,
        };

        fetch('https://localhost:7179/AddCliente', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }
                return response.text();
            })
            .then(result => {
                // Check if the response is not empty
                if (result.trim() !== '') {
                    const jsonData = JSON.parse(result);
                    console.log('Resposta do servidor:', jsonData);
                    // Additional logic as needed
                } else {
                    console.log('Resposta do servidor vazia.');
                }
            })
            .catch(error => {
                console.error('Erro na requisição:', error);
                console.error('Mensagem de erro:', error.message);
            });
    });
});
