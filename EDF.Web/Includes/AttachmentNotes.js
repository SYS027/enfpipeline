
    $(document).ready(function () {
        $("body").on("click", "#saveNote", function () {

            var btn = $(this);
            var retStatus = true;
            $(".has-error").html("");
            var noteComment = $("#noteComment").val();
            var hdnRelevantID = $(".hdnRelevantID").val().trim();
            var hdnPageID = $("#hdnPageID").val().trim();
         
            if (noteComment == "") {
                $("#noteComment").parent().find(".has-error").html("Required*");
                retStatus = false;
            }

 
            if (retStatus == true) {
                var formData = new FormData();
                formData.append("Comment", noteComment);
                formData.append("PageID", hdnPageID);
                formData.append("RelevantID", hdnRelevantID);
                $(btn).attr("data-loading", "");

                var url = "/Notes/AddNote";

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
                            LoadNoteDetails();
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

        $("body").on("click", ".editNote", function () {
            var notetID = $(this).data("noteid");
            $('.noteText[data-noteid="' + notetID + '"]').focus();
        })

        $("body").on("keypress", ".noteText", function () {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                var noteComment = $(this).val().trim();
                if (noteComment == "") {
                    $(this).focus();
                }
                else {
                    var noteID = $(this).data("noteid");
                    var hdnRelevantID = $(".hdnRelevantID").val().trim();
                    var hdnPageID = $("#hdnPageID").val().trim();
                    var formData = new FormData();
                    formData.append("Comment", noteComment);
                    formData.append("PageID", hdnPageID);
                    formData.append("RelevantID", hdnRelevantID);
                    formData.append("NoteID", noteID);

                    var url = "/Notes/UpdateNote";

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
                        },
                        error: function (xhr) {
                            if (xhr.status === 401) {
                                window.location.reload();
                                return;
                            }
                            toastr["error"]("Something went wrong. Please try after some time.");
                        }
                    });
                }
            }
        });

       
        $("body").on("click", ".deleteNote", function () {
            var notetID = $(this).data("noteid");
            var hdnPageID = $("#hdnPageID").val().trim();
            var r = confirm("Are you sure you want to delete this!");
            if (r == true)
            {
               
                $.ajax({
                    url: "/Notes/DeleteNote",
                    type: "post",
                    data: { "PageID": hdnPageID, "NoteID": notetID },
                    dataType: "json",
                    success: function (result) {
                        if (result["Status"] == true) {
                            toastr["success"](result["Message"]);
                            LoadNoteDetails();
                        }
                        else {
                            toastr["error"](result["Message"]);
                        }
                    },
                    error: function (xhr) {
                        if (xhr.status === 401) {
                            window.location.reload();
                            return;
                        }
                    }

                });
            }
        })



        $("body").on("click", "#SaveDoc", function () {

            var btn = $(this);
            var retStatus = true;
            $(".has-error").html("");
            var attachmentType = $("#attachmentType").val();
            var hdnRelevantID = $(".hdnRelevantID").val().trim();
            var hdnPageID = $("#hdnPageID").val().trim();
            var customDocument = $("#customDocument")[0].files;
            if (attachmentType == "") {
                $("#attachmentType").parent().find(".has-error").html("Required*");
                retStatus = false;
            }

            if (customDocument.length == 0) {
                $("#customDocument").parent().find(".has-error").html("Required*");
                retStatus = false;
            }


            if (retStatus == true) {
                var formData = new FormData();
                if (customDocument.length > 0) {
                    formData.append(customDocument[0].name, customDocument[0]);
                }
                formData.append("DocumentID", attachmentType);
                formData.append("PageID", hdnPageID);
                formData.append("RelevantID", hdnRelevantID);
                $(btn).attr("data-loading", "");

                var url = "/Attachments/AddAttachment";

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
                            LoadAttachmentDetails();
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
                $(this).parent().find("label.custom-file-label").html(this.files[0].name);
            }
        });




        $("body").on("click", ".attachmentDelete", function () {
            var attachmentID = $(this).data("attachmentid");
            var hdnPageID = $("#hdnPageID").val().trim();
            var r = confirm("Are you sure you want to delete this!");
            if (r == true) {
                $.ajax({
                    url: "/Attachments/DeleteAttachment",
                    type: "post",
                    data: { "PageID": hdnPageID, "AttachmentID": attachmentID },
                    dataType: "json",
                    success: function (result) {
                        if (result["Status"] == true) {
                            toastr["success"](result["Message"]);
                            LoadAttachmentDetails();
                        }
                        else {
                            toastr["error"](result["Message"]);
                        }
                    },
                    error: function (xhr) {
                        if (xhr.status === 401) {
                            window.location.reload();
                            return;
                        }
                    }

                });
            }
        })
    })



