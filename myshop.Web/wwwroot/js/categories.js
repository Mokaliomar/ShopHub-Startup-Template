$(document).ready(function () {
  $("#mytable").DataTable({
    ajax: {
      /* Original
      url: "/Product/GetData",
      type: "GET",
      dataSrc: "data", */

      // url: "/Admin/Product/GetData",
      url: "/Admin/Category/GetCategories",
      type: "GET",
      dataSrc: "data",
    },
    columns: [
      { data: "name" },
      { data: "description" },
      { data: "createdAt" },
      {
        data: "id",
        render: function (id) {
          /* Original
          return `
                        <a href="/Product/Edit/${id}" class="btn btn-success btn-sm">
                            <i class="fa-solid fa-pen"></i>
                        </a>

                        <button class="btn btn-danger btn-sm">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    `; */
          /* Testing
          return `
                        <a href="/Product/Edit/${id}" class="btn btn-success btn-sm">
                            <i class="fa-solid fa-pen"></i>
                        </a>

                        <a href="/Product/Delete/${id}" class="btn btn-danger btn-sm">
                            <i class="fa-solid fa-trash"></i>
                        </a>
                    `; */
                    return `
                        <a href="/Admin/Category/Edit/${id}" class="btn btn-success btn-sm">
                            <i class="fa-solid fa-pen"></i>
                        </a>

                        <button onclick="DeleteCategory('/Admin/Category/DeleteCategory/${id}')" class="btn btn-danger btn-sm">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    `;
        },
      },
    ],
    autoWidth: false,
    scrollX: true,
  });
});


function DeleteCategory(url) {
  if (confirm("Are you sure to delete this category?")) {
    // رسالة تأكيد بسيطة
    $.ajax({
      url: url,
      type: "POST", // أو POST على حسب أنت كاتب إيه في الـ Controller
      success: function (data) {
        if (data.success) {
          // لو الحذف نجح، بنعمل ريفريش للـ DataTable المفتوح تلقائياً
          $("#mytable").DataTable().ajax.reload();
          alert(data.message); // رسالة نجاح
        } else {
          alert(data.message); // رسالة فشل لو البيزنس منع الحذف
        }
      },
    });
  }
}
