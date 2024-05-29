<script type="text/javascript">
    $(function () {
        $(".PlusProduit").click(function () {
            var recordtoupdate = $(this).attr("data-id");
            if (recordtoupdate !== '') {
                $.post("/Panier/PlusProduit", { "id": recordtoupdate }, function (data) {
                    if (data.ct === 1) {
                        updateCart(data);
                    }
                });
            }
        });

    $(".MinusProduit").click(function () {
        var recordtoupdate = $(this).attr("data-id");
    if (recordtoupdate !== '') {
        $.post("/Panier/MoinsProduit", { "id": recordtoupdate }, function (data) {
            if (data.ct === 1) {
                updateCart(data);
            } else if (data.ct === 0) {
                $("#row-" + recordtoupdate).fadeOut('slow');
            }
        });
        }
    });

    $(".RemoveLink").click(function () {
        var recordtoupdate = $(this).attr("data-id");
    if (recordtoupdate !== '') {
        $.post("/Panier/SupprimerProduit", { "id": recordtoupdate }, function (data) {
            $("#row-" + recordtoupdate).fadeOut('slow');
            $('#totalapayer').text(data.Total);
        });
        }
    });

    function updateCart(data) {
        $('#totalapayer').text(data.Total);
    $("#quantite_" + data.ProduitId).text(data.Quantite);
    $("#rquantite_" + data.ProduitId).text(data.Quantite);
    $("#total_" + data.ProduitId).text(data.TotalRow);
    }
});

</script>