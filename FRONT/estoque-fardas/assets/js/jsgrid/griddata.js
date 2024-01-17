'use strict';

var db = {
    loadData: function () {
        // Make a GET request to the API
        $.get("https://localhost:7179/ListarTodosItens", function (data) {
            // Iterate through the data and populate the table
            $.each(data, function (index, item) {
                var newRow = "<tr data-index='" + index + "'>" +
                    "<td contenteditable='false'>" + item.nome + "</td>" +
                    "<td contenteditable='false'>" + item.tamanho + "</td>" +
                    "<td contenteditable='true'>" + item.preco + "</td>" +
                    "<td contenteditable='true'>" + item.quantidadeRestante + "</td>" +
                    "<td><button class='btn btn-danger' onclick='deleteItem(" + index + ")'>Apagar</button> " +
                    "<button class='btn btn-success' onclick='updateItem(" + index + ")'>Editar</button></td>" +
                    "</tr>";

                // Append the new row to the table
                $("#myTable tbody").append(newRow);
            });
        });
    },
    insertItem: function (insertingClient) {
        // Implement logic to insert item (if needed)
    },
    updateItem: function (index) {
        var editedRow = $("#myTable tbody tr[data-index='" + index + "']");
        var updatedItem = {
            nome: editedRow.find("td:eq(0)").text(),
            tamanho: editedRow.find("td:eq(1)").text(),
            preco: editedRow.find("td:eq(2)").text(),
            quantidadeRestante: editedRow.find("td:eq(3)").text(),
        };

        // Make a PUT request to update the item
        $.ajax({
            url: "https://localhost:7179/EditarItem",
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify(updatedItem),
            success: function () {
                console.log("Item at index " + index + " updated successfully.");
                location.reload();
            },
            error: function () {
                console.error("Error updating item at index " + index);
            }
        });
    },
    deleteItem: function (index) {
        var deletedRow = $("#myTable tbody tr[data-index='" + index + "']");
        var deletedItem = {
            nome: deletedRow.find("td:eq(0)").text(),
            tamanho: deletedRow.find("td:eq(1)").text(),
            // Add other properties as needed based on your API
        };

        // Make a DELETE request to remove the item
        $.ajax({
            url: "https://localhost:7179/RemoverItem",
            type: "DELETE",
            contentType: "application/json",
            data: JSON.stringify(deletedItem),
            success: function () {
                console.log("Item at index " + index + " deleted successfully.");
                // Remove the row from the table after successful deletion
                deletedRow.remove();
            },
            error: function () {
                console.error("Error deleting item at index " + index);
            }
        });
    }
};

window.db = db;

db.loadData();

function updateItem(index) {
    db.updateItem(index);
}

function deleteItem(index) {
    db.deleteItem(index);
}