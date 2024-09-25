
    $(document).ready(function () {
  
        
      

        var customDocumentList = [];
       
        $("body").on("click", "#sendEmail", function () {
            var btn = $(this);
            var retStatus = true;
            $(".has-error").html("");
           
            var toEmail = $("#toEmail").val();
            var ccEmail = $("#ccEmail").val().trim();
            var subject = $("#subject").val();
            var body = $("#txtEditor").parent().find(".Editor-container .Editor-editor").html();

            if (toEmail == "") {
                $("#toEmail").parent().find(".has-error").html("Required*");
                retStatus = false;
            }
            if (subject == "") {
                $("#subject").parent().find(".has-error").html("Required*");
                retStatus = false;
            }
            if (retStatus == true) {
                var formData = new FormData();
                if (customDocumentList.length > 0) {
                    for (var i = 0; i < customDocumentList.length; i++)
                    {
                        formData.append(customDocumentList[i].name, customDocumentList[i]);
                    }
                }
                formData.append("Subject", subject);
                formData.append("toEmailID", toEmail);
                formData.append("ccEmailID", ccEmail);
                formData.append("Body", body);
           
                $(btn).attr("data-loading", "");

                var url = "/ComposeEmail/SendEmail";

                $.ajax({
                    url: url,
                    type: "post",
                    data: formData,
                    dataType: "json",
                    contentType: false,
                    processData: false,
                    success: function (result) {
                        if (result["Status"] == true) {
                            toastr["success"](result["Message"]);
                        }
                        else {
                            toastr["error"](result["Message"]);
                        }
                        $(btn).removeAttr("data-loading");
                    },
                    error: function (xhr) {
                        if (xhr.status === 401) {
                            window.location.reload();
                            return;
                        }
                        $(btn).removeAttr("data-loading");
                        toastr["error"]("Something went wrong. Please try after some time.");
                    }
                });
            }
        })

        $("body").on("change", "#customDocument", function () {
            if (this.files && this.files[0]) {
                var fileName = this.files[0].name;
                $("#attachFileList").append('<div class="attachFileItems" data-type="file" data-name="' + fileName + '"> ' + fileName + ' <span class="fa fa-close"></span></div>');
                customDocumentList.push(this.files[0]);

            }
        });

        $("body").on("click", ".attachFileItems .fa", function () {

            var type = $(this).parent().data("type");
            var name = $(this).parent().data("name");

            if(type=='file')
            {
                for (var i = 0; i < customDocumentList.length; i++) {
                    if(customDocumentList[i].name==name)
                    {
                        customDocumentList.splice(i, 1);
                    }
                }
                console.log(customDocumentList);
            }
            else {
                var attachmentid = $(this).parent().data("attachmentid");
                for (var i = 0; i < attachmentIDs.length; i++) {
                    if (attachmentIDs[i].AttachmentID == attachmentid) {
                        attachmentIDs.splice(i, 1);
                    }
                }
                console.log(attachmentIDs);
            }
            $(this).parent().remove();
        })

  


    })
