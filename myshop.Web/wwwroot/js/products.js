$(document).ready(function () {
  $("#mytable").DataTable({
    ajax: {
      /* Original
      url: "/Product/GetData",
      type: "GET",
      dataSrc: "data", */

      // url: "/Admin/Product/GetData",
      url: "/Product/GetData",
      type: "GET",
      dataSrc: "data",
    },
    columns: [
      { data: "name" },
      { data: "description" },
      { data: "price" },
      { data: "categoryName" },
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
                        <a href="/Product/Edit/${id}" class="btn btn-success btn-sm">
                            <i class="fa-solid fa-pen"></i>
                        </a>

                        <button onclick="DeleteProduct('/Product/Delete/${id}')" class="btn btn-danger btn-sm">
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

// 🚀 الدالة السحرية اللي هتمسح المنتج من غير ريفريش وتحدث الجدول تلقائياً
function DeleteProduct(url) {
    if (confirm("هل أنت متأكد من مسح هذا المنتج؟")) { // رسالة تأكيد بسيطة
        $.ajax({
            url: url,
            type: "DELETE", // أو POST على حسب أنت كاتب إيه في الـ Controller
            success: function (data) {
                if (data.success) {
                    // لو الحذف نجح، بنعمل ريفريش للـ DataTable المفتوح تلقائياً
                    $("#mytable").DataTable().ajax.reload();
                    alert(data.message); // رسالة نجاح
                } else {
                    alert(data.message); // رسالة فشل لو البيزنس منع الحذف
                }
            }
        });
    }
}