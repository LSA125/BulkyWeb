var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": { url: '/admin/product/getall'},
        "columns": [
            { "data": 'title', "width": '20%' },
            { "data": 'isbn', "width": '15%' },
            { "data": 'listPrice', "width": '10%' },
            { "data": 'author', "width": '15%' },
            { "data": 'category.name', "width": '15%' },
            {
                "data": 'id',
                "render": function (data) {
                    return `
                        <div class="text-center btn-group-sm" role="group">
                            <a href="/Admin/Product/Upsert/${data}" class="btn btn-primary">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;"
                                     onClick=Delete('/admin/product/Delete/${data}')>
                                <i class="bi bi-trash"></i> Delete
                            </a>
                        </div>`;
                }, "width": '25%'
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    //show data on console
                    console.log(data);
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}