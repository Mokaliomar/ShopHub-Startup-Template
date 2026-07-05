$(document).ready(function () {
  $("#mytable").DataTable({
    ajax: {
      /* Original
      url: "/Product/GetData",
      type: "GET",
      dataSrc: "data", */

      url: "/Admin/Product/GetData",
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
          return `
                        <a href="/Admin/Product/Edit/${id}" class="btn btn-success btn-sm">
                            <i class="fa-solid fa-pen"></i>
                        </a>

                        <a href="/Admin/Product/Delete/${id}" class="btn btn-danger btn-sm">
                            <i class="fa-solid fa-trash"></i>
                        </a>
                    `;
        },
      },
    ],
    autoWidth: false,
    scrollX: true,
  });
});
