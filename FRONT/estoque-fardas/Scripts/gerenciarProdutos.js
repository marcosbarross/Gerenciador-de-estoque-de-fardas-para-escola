'use strict';

    var db = {
        loadData: function () {
            $.get("https://localhost:7179/ListarTodosItens/", function (data) {
                // Iterate through the data and populate the table
                $.each(data, function (index, item) {
                    var newRow = "<tr data-index='" + index + "'>" +
                        "<td contenteditable='true'>" + item.nome + "</td>" +
                        "<td contenteditable='true'>" + item.tamanho + "</td>" +
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
            };

            $.ajax({
                url: "https://localhost:7179/AtualizarItem/" + index, // Use the appropriate endpoint
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(updatedItem),
                success: function () {
                    console.log("Item at index " + index + " updated successfully.");
                },
                error: function () {
                    console.error("Error updating item at index " + index);
                }
            });
        },
        deleteItem: function (index) {
            console.log("Item at index " + index + " will be deleted.");
        }
    };

    window.db = db;

    db.loadData();

    function updateItem(index) {
        db.updateItem(index);
    }