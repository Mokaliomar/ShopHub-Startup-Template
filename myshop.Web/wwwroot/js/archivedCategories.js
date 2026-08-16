$(document).ready(function () {
  $("#mytable").DataTable({
    ajax: {
      /* Original
      url: "/Product/GetData",
      type: "GET",
      dataSrc: "data", */

      // url: "/Admin/Product/GetData",
      url: "/Admin/Category/GetArchivedCategories",
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
                        <button onclick="RestoreCategory('/Admin/Category/RestoreCategory/${id}')" class="btn btn-success btn-sm">
                            <i class="fa-solid fa-rotate-left"></i> Restore
                        </button>
                    `;
        },
      },
    ],
    autoWidth: false,
    scrollX: true,
  });
});


function RestoreCategory(url) {
  if (confirm("Are you sure to restore this category?")) {
    // رسالة تأكيد بسيطة
    $.ajax({
      url: url,
      type: "PUT",
      /* contentType: 'application/json',
      data: JSON.stringify({ id: id }), // إرسال البيانات في الـ Body */
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
