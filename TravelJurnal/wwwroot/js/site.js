// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


    // JavaScript function to fetch data from API
    function loadEntries() {
        $.ajax({
            type: 'GET',
            url: '/api/EntryAPI/listEntries',
            success: function (data) {
                $('#entries-list').empty();
                $.each(data, function (index, entry) {
                    $('#entries-list').append(`
                        <tr>
                            <td>${entry.entryId}</td>
                            <td>${entry.title}</td>
                            <td>${entry.description}</td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Call the function when the page loads
    $(document).ready(function () {
        loadEntries();
    });
