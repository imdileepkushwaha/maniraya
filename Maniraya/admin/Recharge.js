var MIN_TEXTLENGTH = 4;
//function pageLoad(sender, args) {

//    InitialiseSettings();
//}
function DtHLabel(element) {
    var text = element.options[element.selectedIndex].value;
    var arr = text.split('__');

    document.getElementById('txtDthMsg').innerText = arr[1];
    //document.getElementById('ContentPlaceHolder1_TxtCircel').value = ;
    document.getElementById("ctl00_contentpageData_TxtCardNo").placeholder = arr[2]
}


function getOpCircle(mobile) {
    try {

        if (mobile.length == 4) {
            $.ajax({
                type: "POST",
                url: "../WebService.asmx/getOperatorPrepaid",
                data: '{ "search": "' + mobile + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (opcircle) {                    
                    //lbl1.style.display = 'none';
                    var ddlOper = document.getElementById('ctl00_contentpageData_DdlOpertor');

                    var numJson = JSON.parse(opcircle.d);
                    var oper = numJson[0].Operator.toUpperCase();

                    if (oper == "TELENOR")
                        oper = "UNINOR";
                    var circle = numJson[0].IReffCircle;
                    var OC = numJson[0].IReffOp.toUpperCase();
                    var opt = ddlOper.getElementsByTagName('option');
                    var i, j;
                    j = 0;
                    for (i = 0; i <= opt.length - 1; i++) {
                        if (opt[i].text.indexOf(oper) > -1 && j == 0) {
                            opt[i].setAttribute('Selected', 'Selected');

                            $('#ctl00_contentpageData_TxtiffrC').val($.trim(circle));
                            j = 1;
                        }
                        else
                            opt[i].removeAttribute('Selected');
                        //lbl1.style.display = "none";
                    }
                    $('#' + ddlOper.id).trigger("change");
                },
                error: function (xhr, status, error) {
                    console.log(status + ' ' + error);
                }
            });
            return false;
        }
    } catch (err) {
        console.log("err " + err);
    }
}

function getOpOption(element, txtOption1, txtOption2, txtOption3, txtOption4) {
    try {

        var text1 = element.options[element.selectedIndex].value;
        document.getElementById('Div1').style.display = 'none';
        $.ajax({
            type: "POST",
            url: "../WebService.asmx/GetopertorOption",

            data: '{ "value": "' + text1 + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (opcircle) {
                console.log(opcircle);
                //lbl1.style.display = 'none';
                var ddlOper = document.getElementById('<%=DdlOpertor.SelectedValue %>');
                var numJson = JSON.parse(opcircle.d);
                //var oper = numJson[0].Operator.toUpperCase();
                console.log(txtOption1);
                document.getElementById(txtOption1).placeholder = numJson[0].Displayalue1;

                if (numJson[0].Displayalue2 != "") {
                    console.log(numJson[0].Displayalue2);
                    document.getElementById('Div1').style.display = '';
                    document.getElementById(txtOption2).placeholder = numJson[0].Displayalue2;
                }
                else {

                    document.getElementById('Div1').style.display = 'none';
                }
            },
            error: function (xhr, status, error) {
                console.log(status + ' ' + error);
            }
        });
        return false;
    }
    catch (err) {
        console.log("err " + err);
    }
}
function getOpOption2(element, txtOption1, txtOption2, txtOption3, txtOption4) {
    try {
        var text1 = element.options[element.selectedIndex].value;
       // ContentPlaceHolder1_txtrechageflag.value = '4';
        // var text1 = element.options[element.selectedIndex].value;
        var arr = text1.split('__');
        console.log(arr[0]);
     
        document.getElementById("txtCustomerNo").placeholder = arr[2]
        document.getElementById('Electtrcityoption1').style.display = 'none';
        document.getElementById('Electtrcityoption2').style.display = 'none';
        document.getElementById('Electtrcityoption3').style.display = 'none';
        document.getElementById('Electtrcityoption4').style.display = 'none';

        $.ajax({
            type: "POST",
            url: "../WebService.asmx/GetopertorOption",

            data: '{ "value": "' + arr[0] + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (opcircle) {
                
               
                var ddlOper = document.getElementById(arr[0]);
                var numJson = JSON.parse(opcircle.d);
               
                if (numJson[0].Displayalue1 != "") {
                    console.log(numJson[0].Displayalue2);
                    document.getElementById('Electtrcityoption1').style.display = '';
                    document.getElementById(txtOption1).placeholder = numJson[0].Displayalue1;
                }
                if (numJson[0].Displayalue2 != "") {
                    document.getElementById('Electtrcityoption2').style.display = '';
                    document.getElementById(txtOption2).placeholder = numJson[0].Displayalue2;
                }

                if (numJson[0].Displayalue3 != "") {
                    document.getElementById('Electtrcityoption3').style.display = '';
                    document.getElementById(txtOption3).placeholder = numJson[0].Displayalue3;
                }

                if (numJson[0].Displayalue4 != "") {
                    document.getElementById('Electtrcityoption4').style.display = '';
                    document.getElementById(txtOption4).placeholder = numJson[0].Displayalue4;
                } 
            },
            error: function (xhr, status, error) {
                console.log(status + ' ' + error);
            }
        });
        return false;
    }
    catch (err) {
        console.log("err " + err);
    }
}
function getOpGas(element, txtOption1, txtOption2, txtOption3, txtOption4) {
    try {
        var text1 = element.options[element.selectedIndex].value;
        // ContentPlaceHolder1_txtrechageflag.value = '4';
        // var text1 = element.options[element.selectedIndex].value;
        var arr = text1.split('__');
        console.log(arr[0]);

        document.getElementById("txtGusNo").placeholder = arr[2]
       
        document.getElementById('DivGus1').style.display = 'none';
        document.getElementById('DivGus2').style.display = 'none';
        document.getElementById('DivGus3').style.display = 'none';
        document.getElementById('DivGus4').style.display = 'none';
        $.ajax({
            type: "POST",
            url: "../WebService.asmx/GetopertorOption",

            data: '{ "value": "' + arr[0] + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (opcircle) {

               
                var ddlOper = document.getElementById(arr[0]);
                var numJson = JSON.parse(opcircle.d);
                
                if (numJson[0].Displayalue1 != "") {
                    console.log(document.getElementById(TxtGusOption1));
                   
                    document.getElementById('DivGus1').style.display = '';
                    document.getElementById(txtOption1).placeholder = numJson[0].Displayalue1;
                }
                if (numJson[0].Displayalue2 != "") {
                    document.getElementById('DivGus2').style.display = '';
                    document.getElementById(txtOption2).placeholder = numJson[0].Displayalue2;
                }

                if (numJson[0].Displayalue3 != "") {
                    document.getElementById('DivGus3').style.display = '';
                    document.getElementById(txtOption3).placeholder = numJson[0].Displayalue3;
                }

                if (numJson[0].Displayalue4 != "") {
                    document.getElementById('DivGus4').style.display = '';
                    document.getElementById(txtOption4).placeholder = numJson[0].Displayalue4;
                }
            },
            error: function (xhr, status, error) {
                console.log(status + ' ' + error);
            }
        });
        return false;
    }
    catch (err) {
        console.log("err " + err);
    }
}
function ckeckpostpiad(Id) {

   
    if (Id == 'ctl00_contentpageData_RadioButton2') {
        console.log(Id);
        document.getElementById('ctl00_contentpageData_DdlOpertorPostPaid').style.display = '';
        document.getElementById('ctl00_contentpageData_DdlOpertor').style.display = 'none';
        document.getElementById('Viwelink').style.display = 'none';
    }
    else {
        console.log(Id);
        document.getElementById('Viwelink').style.display = '';
        document.getElementById('ctl00_contentpageData_DdlOpertorPostPaid').style.display = 'none';
        document.getElementById('ctl00_contentpageData_DdlOpertor').style.display = '';
    }
    console.log(Id);
}
function checkPostback(ctrl) {
    if (ctrl != null && ctrl.value && ctrl.value.length >= MIN_TEXTLENGTH && ctrl.value.length <= MIN_TEXTLENGTH) {
        __doPostBack(ctrl.id, '');
    }
}
//function InitialiseSettings() {
//    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
//}

    window.onload = (function (onload) {
          document.getElementById('ctl00_contentpageData_LblNo').value = document.getElementById('ctl00_contentpageData_TxtMobileNo').value;
       // alert(document.getElementById("ctl00_contentpageData_LblNo").value);
   //     document.getElementById("ctl00_contentpageData_LblNo").value = '77777';
        LblAmt.value = document.getElementById('ctl00_contentpageData_TxtAmount').value;
   

        if (document.getElementById('ctl00_contentpageData_RadioButton2').checked == true) {
            LblOp.value = document.getElementById('ctl00_contentpageData_DdlOpertorPostPaid').options[document.getElementById('ctl00_contentpageData_DdlOpertorPostPaid').selectedIndex].text;
    }
    else {
       // LblOp.value = DdlOpertor.options[DdlOpertor.selectedIndex].text;
        LblOp.value = document.getElementById('ctl00_contentpageData_DdlDTHOpertor').options[document.getElementById('ctl00_contentpageData_DdlDTHOpertor').selectedIndex].text;

        
    }
                
      
        if (document.getElementById('ctl00_contentpageData_LblNo').value == "" || LblAmt.value == "" || LblOp.value == "") {
        return false;
    }
    else {
        
            document.getElementById('ctl00_contentScript_ButRecharge').style.display = "none";
    return true;
    }
}(window.onload))
function phoneno() {

    $('ctl00_contentpageData_TxtMobileNo').keypress(function (e) {
        fill();

        var a = [];
        var k = e.which;

        for (i = 48; i < 58; i++)
            a.push(i);

        if (!(a.indexOf(k) >= 0))
            e.preventDefault();

    });
}
function ImageVisbletrue(LblNo1, LblOp, LblAmt, DdlOpertor)
{
    try {
        
        //LblNo1.value = document.getElementById('TxtMobileNo').value;       
        //LblAmt.value = document.getElementById('TxtAmount').value;
        //LblOp.value = DdlOpertor.options[DdlOpertor.selectedIndex].text;
        if (LblNo1.value == "" || LblAmt.value == "" || LblOp.value == "") {           
            document.getElementById('ctl00_contentScript_ButRecharge').style.display = "";
            document.getElementById('loading').style.display = "none";
        }
        else {          
            document.getElementById('ctl00_contentScript_ButRecharge').style.display = "none";
            document.getElementById('loading').style.display = "none";
        }
    }
    catch (error) {
        //console.log(error);
        //alert(error);
        document.getElementById('ctl00_contentScript_ButRecharge').style.display = "";
        document.getElementById('loading').style.display = '';
    }   
}
function fillopenEl(stDCode, MobileNo, Amount, opertor1, flag) {
    try {
        var ddl = document.getElementById(opertor1);
        document.getElementById('ctl00_contentpageData_txtflag').value = flag;
        LblOp.value = ddl.options[ddl.selectedIndex].text;
      
        if (stDCode != "") {
           
            LblNo1.value = document.getElementById(stDCode).value + "-" + document.getElementById(MobileNo).value;
        } else {
            LblNo1.value = document.getElementById(MobileNo).value;
          
        }
        LblAmt.value = document.getElementById(Amount).value;
        document.getElementById('ctl00_contentpageData_txtflag').value = type;

        if (L1.value == "" || L2.value == "" || L3.value == "") {
            return false;
        }
        else {
            return true;
        }
    }
    catch (error) {
        console.log(error);
        return false;
    }
}
function fill2(RechageNo, Amt, opname, type) {
    try {
      //  alert('a');
        document.getElementById('ctl00_contentpageData_txtflag').value = type;
        //var ddl = document.getElementById(opname);
        console.log(RechageNo);
        console.log(Amt);
        console.log(opname);
       // LblOp.value = opname.options[opname.selectedIndex].text;
        //ContentPlaceHolder1_txtrechageflag.value = 2;
        var op = document.getElementById(opname);
      
        LblOp.value = op.options[op.selectedIndex].text;
        LblNo1.value = document.getElementById(RechageNo).value;
        //LblNo1.value = document.getElementById('RechageNo');
        LblAmt.value = document.getElementById(Amt).value;
        // LblOp.value = opname.options[opname.selectedIndex].text;
       // ContentPlaceHolder1_txtrechageflag.value = type;
        if (LblNo.value == "" || LblAmt.value == "" || LblOp.value == "") {
            return false;
        }
        else {
            return true;
        }
    }
    catch (error) {
        console.log(error);
        return false;
    }
}
function fill() {
    try {
        document.getElementById('ctl00_contentpageData_txtflag').value = '1';
        //alert('fill');
        //var dd = document.getElementById('TxtMobileNo');
        //alert(dd);
        LblNo1.value = document.getElementById('ctl00_contentpageData_TxtMobileNo').value;
        //alert(LblNo1.value);
        LblAmt.value = document.getElementById('ctl00_contentpageData_TxtAmount').value;
        if (document.getElementById('ctl00_contentpageData_RadioButton2').checked == true) {
         //   LblOp.value = DdlOpertorPostPaid.options[DdlOpertorPostPaid.selectedIndex].text;
            LblOp.value = document.getElementById('ctl00_contentpageData_DdlOpertorPostPaid').options[document.getElementById('ctl00_contentpageData_DdlOpertorPostPaid').selectedIndex].text;
        }
        else {
           // LblOp.value = DdlOpertor.options[DdlOpertor.selectedIndex].text;
            LblOp.value = document.getElementById('ctl00_contentpageData_DdlOpertor').options[document.getElementById('ctl00_contentpageData_DdlOpertor').selectedIndex].text;

        }
        if (LblNo.value == "" || LblAmt.value == "" || LblOp.value == "") {
            return false;
        }
        else {
            return true;
        }
    }
    catch (error) {
        console.log(error);
        return false;
    }
}

    //function IReff(op) {
    //    var imageid = document.getElementById('OImage');
    //    imageid.src = '../Operatorimage/' + op.toLowerCase() + ".png";
    //    if (op.indexOf("Select") == -1) {
    //        var PrepOp = op.indexOf("DOCOMO") > -1 ? "Docomo" : (op.indexOf("RELIANCE") > -1 ? UpperFirstLetter(op.toLowerCase().split(" ")[0]) + op.split(" ")[1] : ((op.toLowerCase().indexOf("mtnl") > -1) ? op.split(" ")[0] : UpperFirstLetter(op.split(" ")[0].toLowerCase())));
    //        if (PrepOp.toLowerCase() == "bsnl" || PrepOp.toLowerCase() == "mts") {
    //            PrepOp = PrepOp.toUpperCase();
    //        }

    //     var srcIreff = "http://roundpaytech.com/RoundpayPlan.html?opr=" + PrepOp + "&sra="+ $('TxtiffrC').val().trim();
       
        
    //     console.log(srcIreff);
    //     var ifr = document.getElementsByTagName('iframe');
    //        ifr[0].src = srcIreff;
        
    //    }
  
//}


function IReff(op) {
    var imageid = document.getElementById('OImage');
    imageid.src = '../Operatorimage/' + op.toLowerCase() + ".png";
    if (op.indexOf("Select") == -1) {
        var PrepOp = op.indexOf("DOCOMO") > -1 ? "Docomo" : (op.indexOf("RELIANCE") > -1 ? UpperFirstLetter(op.toLowerCase().split(" ")[0]) + op.split(" ")[1] : ((op.toLowerCase().indexOf("mtnl") > -1) ? op.split(" ")[0] : UpperFirstLetter(op.split(" ")[0].toLowerCase())));
        if (PrepOp.toLowerCase() == "bsnl" || PrepOp.toLowerCase() == "mts") {
            PrepOp = PrepOp.toUpperCase();
        }

        //console.log($.trim(val.replace($('TxtiffrC').val, '')));
        //var Seril = $('TxtiffrC').value;
        //$('#TxtiffrC').val($.trim(circle));

        var iddd = document.getElementById('ctl00_contentpageData_TxtiffrC');

       var srcIreff = "http://rechargeplans.co.in/plans.aspx?operator=" + PrepOp + "&sra=" + iddd.value + "";

        // var srcIreff = "http://www.ireff.in/mobilerechargeplans/" + PrepOp + "/" + $('ContentPlaceHolder1_TxtiffrC').val().trim() + "?utm_source=www.skynetcommunication.co.in&utm_medium=web&utm_campaign=widget";
        var ifr = document.getElementsByTagName('iframe');
        ifr[0].src = srcIreff;

    }

}

    function UpperFirstLetter(str) {
        return str.charAt(0).toUpperCase() + str.slice(1);
    }
    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    function Notify(msg, title, type, clear, pos, sticky) {
        if (clear == true) {
            toastr.clear();
        }
        if (sticky == true) {
            toastr.tapToDismiss = true;
            toastr.timeOut = 10000;
        }

        toastr.options.onclick = function () {
            //alert('You can perform some custom action after a toast goes away');
        }
        //"toast-top-left";
        toastr.options.positionClass = pos;

        if (type.toLowerCase() == 'info') {
            toastr.options.timeOut = 10000;
            toastr.tapToDismiss = true;
            toastr.info(msg, title);
        }
        if (type.toLowerCase() == 'success') {
            toastr.options.timeOut = 10000;
            toastr.success(msg, title);
        }
        if (type.toLowerCase() == 'warning') {
            toastr.options.timeOut = 10000;
            toastr.warning(msg, title);
        }
        if (type.toLowerCase() == 'error') {
            toastr.options.timeOut = 10000;
            toastr.error(msg, title);
        }
    }