Option Strict On
Option Explicit On

Imports System
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports Preactor
Imports Preactor.Interop.PreactorObject

<ComVisible(True)>
<Microsoft.VisualBasic.ComClass("06ab901d-c965-4bbd-8858-ab069b00ce7f", "763a65e6-9cc7-4f82-8045-d84ddf2ff6a6")>
Public Class CustomAction
    Public Property tbloReduceQuantiyBulk As DataTable

    Public Function Run(ByRef preactorComObject As PreactorObj, ByRef pespComObject As Object) As Integer

        Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)

        'TODO : Your code goes here

        Return 0
    End Function

    ''---------- Job Spliting -------------
    Public Function K203_JobSplit(ByRef preactorComObject As PreactorObj, ByRef pespComObject As Object, ByRef RecordNumber As Integer) As Integer
        Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)
        Dim strOrderNo As String = preactor.ReadFieldString("Orders", "Order No.", RecordNumber)
        Dim strOrderOprName As String = preactor.ReadFieldString("Orders", "Operation Name", RecordNumber)
        Dim decOrderQty As Double = preactor.ReadFieldDouble("Orders", "Quantity", RecordNumber)
        Dim strSplitJobResource As String = preactor.ReadFieldString("Orders", "Resource", RecordNumber)
        Dim strOrderOprNo As String = preactor.ReadFieldString("Orders", "Op. No.", RecordNumber)
        Dim erpJobNo As String = preactor.ReadFieldString("Orders", "K203_ERPJobNum.", RecordNumber)

        Dim decNewOrderQty As Double = 0
        Dim decBalanceOrderQty As Double = 0

        Dim connetionString As String = preactor.ParseShellString("{DB CONNECT STRING}")

        '' Define the windows form (K203_JobSplitDetails)
        Dim oForm As New K203_JobSplitDetails()

        '' To create suffix details
        Dim K203_OrderSerialRecordNumber As Integer = 0
        Dim strSuffix As String = ""
        Dim intSuffixNo As Integer = 0

        '' ----------Get the length of the order no. ---------------
        Dim intLengthOfJobOrderNo As Integer = strOrderNo.Length()

        Dim strSearchWithinOrderNo As String = strOrderNo
        Dim strSearchThis As String = "#"
        Dim intCharacterEndIndex As Integer = strOrderNo.IndexOf(strSearchThis)

        Dim strAfterHashOrderNo As String = strOrderNo.Substring(intCharacterEndIndex + 1, intLengthOfJobOrderNo - intCharacterEndIndex - 1)

        Dim strOrderNoBeforeHash = strOrderNo.Substring(0, intCharacterEndIndex + 1)

        Dim strParentOrderNumber As String = strOrderNoBeforeHash + strAfterHashOrderNo.Substring(0, 1) + "00"

        Dim strFirstCharOfAfterHashOrderNo = strAfterHashOrderNo.Substring(0, 1)

        strSearchThis = "-"
        Dim intCharEndIndex As Integer = strAfterHashOrderNo.IndexOf(strSearchThis)

        Dim strOrderNoAfterHashBeforeSuffix As String = ""
        If intCharEndIndex > 0 Then
            strOrderNoAfterHashBeforeSuffix = strAfterHashOrderNo.Substring(0, intCharEndIndex)
        Else
            strOrderNoAfterHashBeforeSuffix = strAfterHashOrderNo
        End If

        strOrderNo = strOrderNoBeforeHash + strOrderNoAfterHashBeforeSuffix

        '' To find the latest suffix
        K203_OrderSerialRecordNumber = preactor.FindMatchingRecord("K203_OrderSerial", "ERP Job Num", K203_OrderSerialRecordNumber, erpJobNo)
        Dim intSerial As Integer = 0

        If K203_OrderSerialRecordNumber <= 0 Then
            MsgBox("No relevant record in K203_OrderSerial table ")
        End If

        'intSerial = preactor.ReadFieldInt("K203_OrderSerial", "Serial", K203_OrderSerialRecordNumber)

        '' Call OrderSerial table to get latest serial
        intSerial = GetOrderSerialSeq(connetionString, erpJobNo)

        If K203_OrderSerialRecordNumber > 0 Then
            intSerial = intSerial + 1
        Else
            intSerial = 1
        End If

        Dim strNewOrderNo As String = ""

        If intSerial > 99 Then
            MsgBox("Can't split this job. You have already splited 99 times.")
        Else
            If intSerial > 9 Then
                strSuffix = strFirstCharOfAfterHashOrderNo + intSerial.ToString
            Else
                strSuffix = strFirstCharOfAfterHashOrderNo + "0" + intSerial.ToString
            End If

            strNewOrderNo = strOrderNoBeforeHash + strSuffix

            ''Open job order Form
            oForm.strJobOrderNo = strNewOrderNo
            oForm.decJobOrderQty = decOrderQty
            oForm.isOkClick = False

            '' Open the windows form (K203_JobSplitDetails)
            oForm.ShowDialog()
            Try

                If oForm.isOkClick Then
                    If Not oForm.JobQtyTxt.Text = "" Then
                        decNewOrderQty = CDec(oForm.JobQtyTxt.Text)
                        decBalanceOrderQty = decOrderQty - decNewOrderQty

                        '' Create record for new order number
                        Dim newBlock As Integer = preactor.CreateRecord("Orders")
                        Dim newRecordNum As Integer = preactor.ReadFieldInt("Orders", "Number", newBlock)
                        preactor.CopyRecord("Orders", RecordNumber, newBlock)
                        preactor.WriteField("Orders", "Number", newBlock, newRecordNum)
                        preactor.WriteField("Orders", "Order No.", newBlock, strNewOrderNo)
                        preactor.WriteField("Orders", "Quantity", newBlock, decNewOrderQty)
                        preactor.WriteField("Orders", "K203_Original_Quantity", newBlock, decNewOrderQty)

                        '' Update the Operation sequence - 03-04-2022
                        preactor.WriteField("Orders", "K203_APSOprSeq", newBlock, strSuffix)
                        Dim intK203_ERPOprSeq As Integer = preactor.ReadFieldInt("Orders", "K203_ERPOprSeq", newBlock)
                        Dim intModv As Integer = intK203_ERPOprSeq Mod 100
                        intK203_ERPOprSeq = intK203_ERPOprSeq - intModv
                        Dim strK203_ERPOprSeq As String

                        strK203_ERPOprSeq = (intK203_ERPOprSeq + intSerial).ToString

                        intK203_ERPOprSeq = CInt(strK203_ERPOprSeq)

                        preactor.WriteField("Orders", "K203_ERPOprSeq", newBlock, intK203_ERPOprSeq)
                        '' Update the Order Type 
                        preactor.WriteField("Orders", "Order Type", newBlock, "SPLIT")

                        '' Update the resource for splited job
                        preactor.WriteField("Orders", "Resource", newBlock, strSplitJobResource)

                        '' To update the secondary constraint quantity as zero 
                        Dim strSecondaryConstraintAllocated As String = preactor.ReadFieldString("Orders", "Secondary Constraints", newBlock, 1, 0)

                        ''Dim t2 As Integer = preactor.ReadFieldInt("Orders", "Constraint Quantity", newBlock, 1, 0)
                        ''MsgBox("t2 " & t2)

                        If strSecondaryConstraintAllocated IsNot "" Then
                            '' 23-05-2022 - Remove the secondary Constraints
                            '' 23-05-2022 preactor.WriteMatrixField("Orders", "Constraint Quantity", newBlock, 0, 1, 0)
                            '' 21-05-2022
                            '' 23-05-2022 preactor.WriteMatrixField("Orders", "Secondary Constraints", newBlock, "", 1, 0)
                            preactor.WriteMatrixField("Orders", "Secondary Constraints", newBlock, -1, 1, 0)
                        End If

                        ' To get the Quantity Per Hour - 04-04-2022
                        Dim strPartNo As String = preactor.ReadFieldString("Orders", "Part No.", RecordNumber)
                        Dim intOPNo As Integer = preactor.ReadFieldInt("Orders", "Op. No.", RecordNumber)

                        Dim strQuantityPerHour As String = preactor.ReadFieldString("Orders", "K203_ProdstandardPerSpindle", RecordNumber)
                        Dim decQuantityPerHour As Decimal = CDec(Val("0" & strQuantityPerHour))

                        preactor.WriteField("Orders", "Quantity per Hour", newBlock, decQuantityPerHour)
                        preactor.WriteField("Orders", "K203_AllocatedSpindles", newBlock, 0)
                        preactor.WriteField("Orders", "K203_SpindleStatus", newBlock, "Not Allocated")

                        '' Update original order quantity (Balance)
                        preactor.WriteField("Orders", "Quantity", RecordNumber, decBalanceOrderQty)
                        preactor.WriteField("Orders", "K203_Original_Quantity", RecordNumber, decBalanceOrderQty)

                        If K203_OrderSerialRecordNumber <= 0 Then
                            '' Create record for K203_OrderSerial
                            Dim newBlock_K203_OrderSerial As Integer = preactor.CreateRecord("K203_OrderSerial")
                            preactor.WriteField("K203_OrderSerial", "Order No.", newBlock_K203_OrderSerial, strNewOrderNo)
                            preactor.WriteField("K203_OrderSerial", "Serial", newBlock_K203_OrderSerial, intSerial)
                            preactor.WriteField("K203_OrderSerial", "ERP Job Num", newBlock_K203_OrderSerial, erpJobNo)
                            preactor.WriteField("K203_OrderSerial", "Op. No.", newBlock_K203_OrderSerial, strOrderOprNo)
                        Else
                            '' Update record for K203_OrderSerial
                            preactor.WriteField("K203_OrderSerial", "Order No.", K203_OrderSerialRecordNumber, strNewOrderNo)
                            preactor.WriteField("K203_OrderSerial", "Serial", K203_OrderSerialRecordNumber, intSerial)
                        End If
                        preactor.Commit("K203_OrderSerial")
                        preactor.Commit("Orders")
                    End If

                    preactor.Redraw()
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

        Return 0
    End Function
    ''GetLatestOprSequnace
    Public Function GetLatestOprSequnace(ByRef connetionString As String, ByRef orderNoPrefix As String, ByRef operationName As String) As Integer

        Try
            '' MsgBox("orderNoPrefix - TTT - " & orderNoPrefix & " " & operationName)

            Dim connection As SqlConnection
            Dim adapter As SqlDataAdapter
            Dim command As New SqlCommand

            connection = New SqlConnection(connetionString)

            connection.Open()
            command.Connection = connection
            command.CommandType = CommandType.StoredProcedure
            'command.CommandText = "K201_GetAvailableFormer_Sp"
            command.CommandText = "K203_GetLatestOprSequnaceByOrderPrefixAndOperationCode_Sp"
            '
            command.CommandTimeout = 600

            Dim param As SqlParameter

            param = New SqlParameter("@OrderNoPrefix", orderNoPrefix)
            param.Direction = ParameterDirection.Input
            param.DbType = DbType.String
            command.Parameters.Add(param)

            param = New SqlParameter("@OperationName", operationName)
            param.Direction = ParameterDirection.Input
            param.DbType = DbType.String
            command.Parameters.Add(param)


            '' 18_02_2022
            ''Dim availableFormer As Decimal = 0
            ''param = New SqlParameter("@MaxSequance", availableFormer)
            ''param.Direction = ParameterDirection.Output
            ''param.DbType = DbType.Decimal
            ''command.Parameters.Add(param)

            '' Dim availableMaxSeq As Decimal = 0
            Dim availableMaxSeq As Integer
            param = New SqlParameter("@MaxSequance", availableMaxSeq)
            param.Direction = ParameterDirection.Output
            param.DbType = DbType.Int64
            command.Parameters.Add(param)

            adapter = New SqlDataAdapter(command)
            command.ExecuteNonQuery()

            If Not (param.Value.ToString = "0") Then
                Return CInt(param.Value)
            Else
                Return 0
            End If
            connection.Close()
        Catch ex As Exception
            '' 18_02_2022
            '' MsgBox("Available former not define",, "error")
            MsgBox(ex.Message)
        Finally

        End Try
    End Function
    ''GetLatestOprSeqRecoreId
    Public Function GetLatestOprSeqRecoreId(ByRef connetionString As String, ByRef orderNoPrefix As String, ByRef operationName As String) As Integer

        Try
            Dim connection As SqlConnection
            Dim adapter As SqlDataAdapter
            Dim command As New SqlCommand

            connection = New SqlConnection(connetionString)

            connection.Open()
            command.Connection = connection
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "K203_OprSeqRecIdByOrderPrefixAndOperationCode_Sp"

            command.CommandTimeout = 600

            Dim param As SqlParameter

            param = New SqlParameter("@OrderNoPrefix", orderNoPrefix)
            param.Direction = ParameterDirection.Input
            param.DbType = DbType.String
            command.Parameters.Add(param)

            param = New SqlParameter("@OperationName", operationName)
            param.Direction = ParameterDirection.Input
            param.DbType = DbType.String
            command.Parameters.Add(param)


            '' 18_02_2022
            ''Dim availableFormer As Decimal = 0
            ''param = New SqlParameter("@MaxSequance", availableFormer)
            ''param.Direction = ParameterDirection.Output
            ''param.DbType = DbType.Decimal
            ''command.Parameters.Add(param)

            '' Dim availableMaxSeq As Decimal = 0
            Dim availableMaxSeq As Integer = 0
            param = New SqlParameter("@RecordId", availableMaxSeq)
            param.Direction = ParameterDirection.Output
            param.DbType = DbType.Int64
            command.Parameters.Add(param)

            adapter = New SqlDataAdapter(command)
            command.ExecuteNonQuery()

            If Not (param.Value.ToString = "0") Then
                Return CInt(param.Value)
            Else
                Return 0
            End If
            connection.Close()
        Catch ex As Exception
            '' 18_02_2022
            '' MsgBox("Available former not define",, "error")
            MsgBox(ex.Message)
        Finally

        End Try
    End Function

    ''---------- Spindle Allocation -------------
    Public Function K203_SpindleAllocation(ByRef preactorComObject As PreactorObj, ByRef pespComObject As Object, ByRef RecordNumber As Integer) As Integer
        Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)
        Dim pb As IPlanningBoard = preactor.PlanningBoard

        Dim dtStartTime As DateTime = preactor.ReadFieldDateTime("Orders", "Start Time", RecordNumber) '' 12-05-2022
        Dim intStartYear As Integer = dtStartTime.Year '' 12-05-2022

        If intStartYear > 2000 Then '' "2000" default date
            Dim strOrderNo As String = preactor.ReadFieldString("Orders", "Order No.", RecordNumber)
            Dim strOpNo As Integer = preactor.ReadFieldInt("Orders", "Op. No.", RecordNumber)

            Dim connetionString As String = preactor.ParseShellString("{DB CONNECT STRING}")

            Dim strResourceGroup As String = preactor.ReadFieldString("Orders", "Resource Group", RecordNumber)
            Dim strResource As String = preactor.ReadFieldString("Orders", "Resource", RecordNumber)

            '' Dim dtToday As Date = Today - 21-03-2022

            Dim dtToday As DateTime = preactor.ReadFieldDateTime("Orders", "Setup Start", RecordNumber)

            Dim intK203_ResourceSecondaryConstraintRecNumber As Integer = preactor.FindMatchingRecord("K203_ResourceSecondaryConstraint", "Resources", intK203_ResourceSecondaryConstraintRecNumber, strResource)

            '' 28-06-2022
            If intK203_ResourceSecondaryConstraintRecNumber <= 0 Then
                MsgBox("No relevant record in K203_ResourceSecondaryConstraint table")
            End If

            Dim strSecondaryConstraint As String = preactor.ReadFieldString("K203_ResourceSecondaryConstraint", "Secondary Constraints", intK203_ResourceSecondaryConstraintRecNumber)

            Dim intSecondaryConstraintRecNumber As Integer = preactor.FindMatchingRecord("Secondary Constraints", "Name", intSecondaryConstraintRecNumber, strSecondaryConstraint)

            '' 15-06-2022 Dim totalSpindle As Integer = GetTotalSpindle(connetionString, intSecondaryConstraintRecNumber)

            '' 15-06-2022 - Siart - Aamir
            Dim totalSpindle As Integer = GetMaxSpindle(connetionString, intSecondaryConstraintRecNumber, dtStartTime)

            '' 15-06-2022 - End -


            Dim dblSpindleUsage As Double
            dblSpindleUsage = pb.GetSecondaryResourceCurrentState(intSecondaryConstraintRecNumber, dtToday).CurrentValue
            Dim constraint_Group_1 As String
            Dim constraint_Group_2 As String
            Dim constraint_Group_1_val As Double
            Dim constraint_Group_2_val As Double
            Dim constraint_Group_1_num As Integer
            Dim constraint_Group_2_num As Integer
            Dim constraint_Group_1_Maxval As Integer
            Dim constraint_Group_2_Maxval As Integer
            ''Constraint Group 1
            constraint_Group_1 = preactor.ReadFieldString("Orders", "Constraint Group 1", RecordNumber)
            constraint_Group_1_num = preactor.FindMatchingRecord("Secondary Constraints", "Name", constraint_Group_1_num, constraint_Group_1)
            If constraint_Group_1_num > 0 Then
                constraint_Group_1_Maxval = GetMaxSpindle(connetionString, constraint_Group_1_num, dtStartTime)
                constraint_Group_1_val = pb.GetSecondaryResourceCurrentState(constraint_Group_1_num, dtToday).CurrentValue
            End If

            ''Constraint Group 2
            constraint_Group_2 = preactor.ReadFieldString("Orders", "Constraint Group 2", RecordNumber)
            constraint_Group_2_num = preactor.FindMatchingRecord("Secondary Constraints", "Name", constraint_Group_2_num, constraint_Group_2)
            If constraint_Group_1_num > 0 Then
                constraint_Group_2_Maxval = GetMaxSpindle(connetionString, constraint_Group_2_num, dtStartTime)
                constraint_Group_2_val = pb.GetSecondaryResourceCurrentState(constraint_Group_2_num, dtToday).CurrentValue
            End If
            ''----------
            '' MsgBox("dblSpindleUsage " & dblSpindleUsage & " totalSpindle " & totalSpindle & " dtToday " & dtToday)

            Dim intAvailableSpindels As Double = totalSpindle - dblSpindleUsage
            If strOpNo = 60 Then
                Dim intconstraint_Group_1_AvbVal As Double = constraint_Group_1_Maxval - constraint_Group_1_val
                Dim intconstraint_Group_2_AvbVal As Double = constraint_Group_2_Maxval - constraint_Group_2_val
                If intconstraint_Group_2_AvbVal > intconstraint_Group_1_AvbVal Then
                    intconstraint_Group_2_AvbVal = intconstraint_Group_1_AvbVal
                End If
                If intconstraint_Group_2_AvbVal > 0 Then
                    If intAvailableSpindels > intconstraint_Group_2_AvbVal Then
                        intAvailableSpindels = intconstraint_Group_2_AvbVal
                    End If
                End If
            End If
            Dim StrSpindleStatus As String = preactor.ReadFieldString("Orders", "K203_SpindleStatus", RecordNumber)
            Dim intRunningSpindleQty As Integer = 0
            If StrSpindleStatus = "Allocated" Then
                intRunningSpindleQty = preactor.ReadFieldInt("Orders", "Constraint Quantity", RecordNumber, 1, 0)
            End If

            '' Define the windows form (K203_SpindleAllocation) 
            Dim oForm As New K203_SpindleAllocation()
            oForm.strJobOrderNo = strOrderNo
            oForm.strAvailableSpindels = intAvailableSpindels.ToString
            oForm.strRunningSpindleQty = intRunningSpindleQty.ToString
            oForm.strMaxSpindles = totalSpindle.ToString

            '' To open the windows form (K203_SpindleAllocation) 
            oForm.ShowDialog()

            '' To store spindle allocate quantity
            Try
                If oForm.isOkClick Then
                    Dim intAllocateSpindleQuantity As Integer = CInt(Int(oForm.AllocateSpindlesTxt.Text))
                    Dim strSecondaryConstraintAllocated As String = preactor.ReadFieldString("Orders", "Secondary Constraints", RecordNumber, 1, 0)

                    If strSecondaryConstraintAllocated = "" Then
                        Dim mydictionary As New Dictionary(Of String, Double)
                        Dim matdimensions As MatrixDimensions = preactor.MatrixFieldSize("Orders", "Secondary Constraints", RecordNumber)

                        mydictionary.Add(strSecondaryConstraint, intAllocateSpindleQuantity)

                        '' +++++++++++++++++++ Writting to data to Matrix field

                        preactor.SetAutoListSize("Orders", "Secondary Constraints", RecordNumber, mydictionary.Count)
                        Dim count As Integer = 0
                        For Each kvp As KeyValuePair(Of String, Double) In mydictionary
                            count += 1
                            preactor.WriteListField("Orders", "Secondary Constraints", RecordNumber, kvp.Key, count)
                            preactor.WriteListField("Orders", "Constraint Quantity", RecordNumber, kvp.Value, count)

                            ''MsgBox(" kvp.Key - kvp.Value " & kvp.Key & " - " & kvp.Value)

                            '' preactor.Commit("Orders")
                            '' preactor.Redraw()

                        Next

                    Else
                        preactor.WriteMatrixField("Orders", "Secondary Constraints", RecordNumber, strSecondaryConstraint, 1, 0)
                        preactor.WriteMatrixField("Orders", "Constraint Quantity", RecordNumber, intAllocateSpindleQuantity, 1, 0)

                        '' preactor.Commit("Orders")
                        '' preactor.Redraw()
                        '' preactor.ref
                        '' MsgBox("LLL")
                    End If
                    preactor.Commit("Orders")
                    preactor.Redraw()
                    '' dblSpindleUsage = pb.GetSecondaryResourceCurrentState(intSecondaryConstraintRecNumber, dtToday).CurrentValue
                    '' MsgBox(dblSpindleUsage)
                    '' 22-04-2022
                    '' Max. Pack Size --> Numerical Attribute 1
                    Dim intMaxPackSize As Integer = preactor.ReadFieldInt("Orders", "Numerical Attribute 1", RecordNumber)
                    '' No. Of Hrs. Per Doff --> Numerical Attribute 2
                    Dim strNoOfHrsPerDoff As String = preactor.ReadFieldString("Orders", "Numerical Attribute 2", RecordNumber)
                    Dim decNoOfHrsPerDoff As Decimal = CDec(Val("0" & strNoOfHrsPerDoff))
                    ''Milan Amarasooriya 20220804 befor
                    ''Dim strSetupTime As String = preactor.ReadFieldString("Orders", "Setup Time", RecordNumber)
                    ''Milan Amarasooriya 20220804 after
                    Dim strSetupTime As String = preactor.ReadFieldString("Orders", "Duration Attribute 1", RecordNumber)
                    '' To find the Hours
                    Dim intHIndex As Integer = strSetupTime.IndexOf("H")
                    Dim strHours = strSetupTime.Substring(0, intHIndex)
                    Dim decHours As Decimal = CDec(Val("0" & strHours))

                    ''To find the minutes
                    Dim intMIndex As Integer = strSetupTime.IndexOf("M")
                    Dim strMinutes = strSetupTime.Substring(intMIndex - 3, intHIndex)
                    Dim decMinutes As Decimal = CDec(Val("0" & strMinutes))
                    Dim decSetupTime = decHours + decMinutes / 60
                    Dim decOpStandard As Decimal = (intMaxPackSize) / (decNoOfHrsPerDoff + decSetupTime)
                    ''Milan Amarasooriya 20220804 
                    If decSetupTime = 0 Then
                        MsgBox("Scheduled without Doff Setup time",, "Information")
                    End If
                    Dim strPartNo As String = preactor.ReadFieldString("Orders", "Part No.", RecordNumber)

                    Dim intOPNo As Integer = preactor.ReadFieldInt("Orders", "Op. No.", RecordNumber)

                    '' To get the Quantity Per Hour
                    '' 21-05-2022 - Dim decQuantityPerHour As Decimal = GetQuantityPerHour(connetionString, strPartNo, intOPNo)  
                    '' 21-05-22
                    Dim strQuantityPerHour As String = preactor.ReadFieldString("Orders", "K203_ProdstandardPerSpindle", RecordNumber)
                    Dim decQuantityPerHour As Decimal = CDec(Val("0" & strQuantityPerHour))

                    '' MsgBox("decQuantityPerHour " & decQuantityPerHour)


                    ''29-04-2022
                    ''"Prodstandard per Spindle" is equal to "intQuantityPerHour"
                    Dim decProdstandardPerSpindle As Decimal = decQuantityPerHour

                    If intAllocateSpindleQuantity > 0 Then
                        preactor.WriteField("Orders", "K203_AllocatedSpindles", RecordNumber, intAllocateSpindleQuantity)

                        ''29-04-2022 - Writting the Doff Qty ("Doff Qty" is equal to "Transfer Quantity") 
                        '' 21-05-2022 - preactor.WriteField("Orders", "K203_ProdstandardPerSpindle", RecordNumber, decQuantityPerHour)
                        Dim decDoffQty As Decimal = intAllocateSpindleQuantity * decProdstandardPerSpindle * decNoOfHrsPerDoff
                        preactor.WriteField("Orders", "Transfer Quantity", RecordNumber, decDoffQty)
                        Dim decQtyPerHourForSpindles As Decimal = (intAllocateSpindleQuantity * decProdstandardPerSpindle * decNoOfHrsPerDoff) / (decNoOfHrsPerDoff + decSetupTime)

                        '' MsgBox("AAA decQtyPerHourForSpindles " & decQtyPerHourForSpindles)

                        preactor.WriteField("Orders", "Quantity per Hour", RecordNumber, decQtyPerHourForSpindles)
                        preactor.WriteField("Orders", "K203_SpindleStatus", RecordNumber, "Allocated")
                    Else
                        preactor.WriteField("Orders", "K203_AllocatedSpindles", RecordNumber, 0)
                        preactor.WriteField("Orders", "Quantity per Hour", RecordNumber, decQuantityPerHour)
                        preactor.WriteField("Orders", "K203_SpindleStatus", RecordNumber, "Not Allocated")
                    End If

                    '' MsgBox("AAA")
                    preactor.Commit("Orders")
                    preactor.Redraw()
                    MsgBox("Successfully completed")

                    '' dblSpindleUsage = pb.GetSecondaryResourceCurrentState(intSecondaryConstraintRecNumber, dtToday).CurrentValue
                    '' MsgBox("dblSpindleUsage " & dblSpindleUsage)

                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            '' preactor.Commit("Orders")
            '' preactor.Redraw()

        Else
            MsgBox("Job not schedule")
        End If

        '' preactor.Commit("Orders")
        '' preactor.Redraw()

        '' planningboard.Close()
        '' planningboard.SetLocateState(False)
        '' pb.Close()
        '' pb.SetLocateState(False)
        Return 0
    End Function



    '' 15-06-2022 - Start - Aamir

    Public Function GetMaxSpindle(ByRef connetionString As String, ByRef intSecondaryConstraintRecNumber As Integer, ByRef dtStartTime As Date) As Integer

        Try
            Dim connection As SqlConnection
            Dim adapter As SqlDataAdapter
            Dim command As New SqlCommand

            connection = New SqlConnection(connetionString)

            connection.Open()
            command.Connection = connection
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "K203_GetMaxSpindle_Sp"
            '
            command.CommandTimeout = 600

            Dim param As SqlParameter

            param = New SqlParameter("@secondaryConstraintNumber", intSecondaryConstraintRecNumber)
            param.Direction = ParameterDirection.Input
            param.DbType = DbType.Int64
            command.Parameters.Add(param)

            param = New SqlParameter("@StartDate", dtStartTime)
            param.Direction = ParameterDirection.Input
            param.DbType = DbType.Date
            command.Parameters.Add(param)

            Dim intTotalSpindle As Integer = 0
            param = New SqlParameter("@MaxValue", intTotalSpindle)
            param.Direction = ParameterDirection.Output
            param.DbType = DbType.Int64
            command.Parameters.Add(param)

            adapter = New SqlDataAdapter(command)
            command.ExecuteNonQuery()

            If Not (param.Value.ToString = "0") Then
                Return CInt(param.Value)
            Else
                Return 0
            End If
            connection.Close()
        Catch ex As Exception
            '' 18_02_2022
            '' MsgBox("Available former not define",, "error")
            MsgBox(ex.Message)
        Finally

        End Try
    End Function

    '' 15-06-2022 - End - Aamir

    Public Function GetQuantityPerHour(ByRef connetionString As String, ByRef strPartNo As String, ByRef intOPNo As Integer) As Decimal

        Try
            Dim connection As SqlConnection
            Dim adapter As SqlDataAdapter
            Dim command As New SqlCommand

            connection = New SqlConnection(connetionString)

            connection.Open()
            command.Connection = connection
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "K203_GetQuantityPerHour_Sp"
            '
            command.CommandTimeout = 600

            Dim param As SqlParameter

            param = New SqlParameter("@PartNo", strPartNo)
            param.Direction = ParameterDirection.Input
            param.DbType = DbType.String
            command.Parameters.Add(param)

            param = New SqlParameter("@OPNo", intOPNo)
            param.Direction = ParameterDirection.Input
            param.DbType = DbType.Int64
            command.Parameters.Add(param)

            Dim decQuantityPerHour As Double
            param = New SqlParameter("@QuantityPerHour", decQuantityPerHour)
            param.Direction = ParameterDirection.Output
            param.DbType = DbType.Double
            '' param.DbType = DbType.Decimal
            command.Parameters.Add(param)

            adapter = New SqlDataAdapter(command)
            command.ExecuteNonQuery()
            ''MsgBox("CDec(param.Value) " & CDec(param.Value))
            ''MsgBox("param.Value.ToString " & param.Value.ToString)
            If Not (param.Value.ToString = "") Then
                '' Return CInt(param.Value)
                Return CDec(param.Value)
            Else
                Return 0
            End If
            connection.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

        End Try
    End Function

    ''25-04-2022
    Public Function K203_DragDrop(ByRef preactorComObject As PreactorObj, ByRef pespComObject As Object, ByRef RecordNumber As Integer) As Integer

        Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)
        Dim connetionString As String = preactor.ParseShellString("{DB CONNECT STRING}")

        Dim strSpindleStatus As String = preactor.ReadFieldString("Orders", "K203_SpindleStatus", RecordNumber)

        '' 26-05-2022 If strSpindleStatus = "Not Allocated" Then
        ''MsgBox("strSpindleStatus " & strSpindleStatus)

        Dim strResource As String = preactor.ReadFieldString("Orders", "Resource", RecordNumber)

        If strResource <> "Unspecified" Then '' 04-06-2022 - Move to the planing board

            Dim intK203_ResourceSecondaryConstraintRecNumber As Integer = preactor.FindMatchingRecord("K203_ResourceSecondaryConstraint", "Resources", intK203_ResourceSecondaryConstraintRecNumber, strResource)

            Dim strSecondaryConstraint As String = preactor.ReadFieldString("K203_ResourceSecondaryConstraint", "Secondary Constraints", intK203_ResourceSecondaryConstraintRecNumber)

            Dim intSecondaryConstraintRecNumber As Integer = preactor.FindMatchingRecord("Secondary Constraints", "Name", intSecondaryConstraintRecNumber, strSecondaryConstraint)

            '' To get the total spendle quantity (Maximum number of spindles)
            '' 15-06-2022 - UH
            '' Dim intTotalSpindle As Integer = GetTotalSpindle(connetionString, intSecondaryConstraintRecNumber)

            Dim dtStartTime As DateTime = preactor.ReadFieldDateTime("Orders", "Start Time", RecordNumber) '' 15-06-2022

            '' 15-06-2022 - Siart - Aamir
            Dim totalSpindle As Integer = GetMaxSpindle(connetionString, intSecondaryConstraintRecNumber, dtStartTime)

            '' 15-06-2022 - End -

            ''29-04-2022
            Dim strPartNo As String = preactor.ReadFieldString("Orders", "Part No.", RecordNumber)
            Dim intOPNo As Integer = preactor.ReadFieldInt("Orders", "Op. No.", RecordNumber)
            '' To get the Quantity Per Hour
            ''Commende by  Milan amarasooriya 20220729 Start
            'Dim decQuantityPerHour As Decimal = GetQuantityPerHour(connetionString, strPartNo, intOPNo)
            'Dim decProdstandardPerSpindle As Decimal = decQuantityPerHour
            ''Commende by  Milan amarasooriya 20220729 End
            Dim decProdstandardPerSpindle As Double = preactor.ReadFieldDouble("Orders", "K203_ProdstandardPerSpindle", RecordNumber)

            '' Max. Pack Size --> Numerical Attribute 1
            Dim intMaxPackSize As Integer = preactor.ReadFieldInt("Orders", "Numerical Attribute 1", RecordNumber)
            '' No. Of Hrs. Per Doff --> Numerical Attribute 2
            Dim strNoOfHrsPerDoff As String = preactor.ReadFieldString("Orders", "Numerical Attribute 2", RecordNumber)
            Dim decNoOfHrsPerDoff As Decimal = CDec(Val("0" & strNoOfHrsPerDoff))
            ''Milan Amarasooriya 20220804 befor
            ''Dim strSetupTime As String = preactor.ReadFieldString("Orders", "Setup Time", RecordNumber)
            ''Milan Amarasooriya 20220804 After
            Dim strSetupTime As String = preactor.ReadFieldString("Orders", "Duration Attribute 1", RecordNumber)

            '' To find the Hours
            Dim intHIndex As Integer = strSetupTime.IndexOf("H")
            Dim strHours = strSetupTime.Substring(0, intHIndex)
            Dim decHours As Decimal = CDec(Val("0" & strHours))
            ''To find the minutes
            Dim intMIndex As Integer = strSetupTime.IndexOf("M")
            Dim strMinutes = strSetupTime.Substring(intMIndex - 3, intHIndex)
            Dim decMinutes As Decimal = CDec(Val("0" & strMinutes))
            Dim decSetupTime = decHours + decMinutes / 60
            '' - 29-04-2022 - Dim decOpStandard As Decimal = (intMaxPackSize) / (decNoOfHrsPerDoff + decSetupTime)
            '' 29-04-2022
            '' 15-06-2022 - Dim decQtyPerHourForSpindles As Decimal = (intTotalSpindle * decProdstandardPerSpindle * decNoOfHrsPerDoff) / (decNoOfHrsPerDoff + decSetupTime)

            '' 15-06-2022 - Start
            Dim decQtyPerHourForSpindles As Double = (totalSpindle * decProdstandardPerSpindle * decNoOfHrsPerDoff) / (decNoOfHrsPerDoff + decSetupTime)
            '' 15-06-2022 - End

            '' 26-05-2022

            Dim strOldSecondaryConstraint As String = preactor.ReadFieldString("Orders", "Secondary Constraints", RecordNumber, 1, 0)

            Dim strOldResource As String = "Old Resource"

            If strOldSecondaryConstraint <> "Unspecified" And strOldSecondaryConstraint <> "" Then
                Dim intK203_OldSecondaryConstraintRecNumber As Integer = preactor.FindMatchingRecord("K203_ResourceSecondaryConstraint", "Secondary Constraints", intK203_OldSecondaryConstraintRecNumber, strOldSecondaryConstraint)
                strOldResource = preactor.ReadFieldString("K203_ResourceSecondaryConstraint", "Resources", intK203_OldSecondaryConstraintRecNumber)
            End If

            Dim strCurrentResource As String = preactor.ReadFieldString("Orders", "Resource", RecordNumber)

            '' 26-05-2022
            If strSpindleStatus = "Not Allocated" Then

                preactor.WriteField("Orders", "Quantity per Hour", RecordNumber, decQtyPerHourForSpindles)

                If strOldResource <> strCurrentResource Then

                    If strOldSecondaryConstraint <> "Unspecified" And strOldSecondaryConstraint <> "" Then
                        preactor.WriteMatrixField("Orders", "Secondary Constraints", RecordNumber, -1, 1, 0)
                    End If

                End If

            Else
                If strOldResource <> strCurrentResource Then  '' 02-06-2022 Resources change in planing board
                    preactor.WriteMatrixField("Orders", "Secondary Constraints", RecordNumber, -1, 1, 0)
                    preactor.WriteField("Orders", "Quantity per Hour", RecordNumber, decQtyPerHourForSpindles)
                    preactor.WriteField("Orders", "K203_AllocatedSpindles", RecordNumber, 0)
                    preactor.WriteField("Orders", "K203_SpindleStatus", RecordNumber, "Not Allocated")
                End If
            End If
        Else '' 04-02-2022 - Removing from planing board

            preactor.WriteMatrixField("Orders", "Secondary Constraints", RecordNumber, -1, 1, 0)
            Dim strQuantityPerHour As String = preactor.ReadFieldString("Orders", "K203_ProdstandardPerSpindle", RecordNumber)
            Dim decQuantityPerHour As Decimal = CDec(Val("0" & strQuantityPerHour))
            preactor.WriteField("Orders", "Quantity per Hour", RecordNumber, decQuantityPerHour)
            preactor.WriteField("Orders", "K203_AllocatedSpindles", RecordNumber, 0)
            preactor.WriteField("Orders", "K203_SpindleStatus", RecordNumber, "Not Allocated")

        End If

        Return 0
    End Function
    '' 04-02-2022 - Removing from planing board
    Public Function K203_OnUnallocate(ByRef preactorComObject As PreactorObj, ByRef pespComObject As Object, ByRef RecordNumber As Integer) As Integer
        Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)
        Dim planningboard As IPlanningBoard = preactor.PlanningBoard
        Dim num As Integer = preactor.RecordCount("Orders")
        Dim RecordNumber_ As Integer = 0
        Dim i As Integer = 1
        Dim count As Integer = 0

        Do
            If (planningboard.GetOperationLocateState(i)) Then
                count = count + 1
            End If
            i = i + 1
        Loop While i <= num
        If count = num Then
            ''   MsgBox("Secondary constraints are not removed from the group of Unallocate All, If need Highlight first and retry!",, "Information")
        Else
            i = 1
            Do
                If (planningboard.GetOperationLocateState(i)) Then
                    Dim OrderNo_ = preactor.ReadFieldString("Orders", "Order No.", i)
                    RecordNumber_ = preactor.FindMatchingRecord("Orders", "Order No.", RecordNumber_, OrderNo_)

                    Dim secodryconstrain As String = preactor.ReadFieldString("Orders", "Secondary Constraints", RecordNumber_, 1, 0)
                    If Not (secodryconstrain = "") Then
                        preactor.WriteMatrixField("Orders", "Secondary Constraints", RecordNumber_, -1, 1, 0)
                    End If

                    Dim strQuantityPerHour As String = preactor.ReadFieldString("Orders", "K203_ProdstandardPerSpindle", RecordNumber_)
                    Dim decQuantityPerHour As Decimal = CDec(Val("0" & strQuantityPerHour))

                    preactor.WriteField("Orders", "Quantity per Hour", RecordNumber_, decQuantityPerHour)
                    preactor.WriteField("Orders", "K203_AllocatedSpindles", RecordNumber_, 0)
                    preactor.WriteField("Orders", "K203_SpindleStatus", RecordNumber_, "Not Allocated")

                End If
                i = i + 1
            Loop While i <= num
        End If

        ''Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)

        'preactor.WriteMatrixField("Orders", "Secondary Constraints", RecordNumber, -1, 1, 0)
        'Dim strQuantityPerHour As String = preactor.ReadFieldString("Orders", "K203_ProdstandardPerSpindle", RecordNumber)
        'Dim decQuantityPerHour As Decimal = CDec(Val("0" & strQuantityPerHour))


        'preactor.WriteField("Orders", "Quantity per Hour", RecordNumber, decQuantityPerHour)

        'preactor.WriteField("Orders", "K203_AllocatedSpindles", RecordNumber, 0)

        'preactor.WriteField("Orders", "K203_SpindleStatus", RecordNumber, "Not Allocated")

        Return 0
    End Function
    Public Function K203_OnUnallocateAll(ByRef preactorComObject As PreactorObj, ByRef pespComObject As Object) As Integer
        Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)
        Dim planningboard As IPlanningBoard = preactor.PlanningBoard
        Dim num As Integer = preactor.RecordCount("Orders")
        Dim i As Integer = 1
        Dim count As Integer = 0

        Do
            If (planningboard.GetOperationLocateState(i)) Then
                count = count + 1
            End If
            i = i + 1
        Loop While i <= num
        If count = num Then
            MsgBox("Secondary constraints are not removed from the group of Unallocate All, If need Highlight first and retry!",, "Information")
        End If
    End Function
    '' 09-06-2022 - Job Merge
    ''K203_JobMerge
    Public Function K203_JobMerge(ByRef preactorComObject As PreactorObj, ByRef pespComObject As Object, ByRef RecordNumber As Integer) As Integer

        Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)
        Dim planningboard As IPlanningBoard = preactor.PlanningBoard
        Dim connetionString As String = preactor.ParseShellString("{DB CONNECT STRING}")

        Dim strOrderNo As String = preactor.ReadFieldString("Orders", "Order No.", RecordNumber)
        Dim dblOrderQty As Double = preactor.ReadFieldDouble("Orders", "Quantity", RecordNumber)
        Dim strResource As String = preactor.ReadFieldString("Orders", "Resource", RecordNumber)
        Dim dteSetupStart As DateTime = preactor.ReadFieldDateTime("Orders", "Setup Start", RecordNumber)

        Dim sK203_AllocatedSpindles As Double = preactor.ReadFieldDouble("Orders", "K203_AllocatedSpindles", RecordNumber)
        If sK203_AllocatedSpindles = 0 Then
            MsgBox("Spindle allocation is required before merging",, "Infor")
        Else
            Dim dblTotalAssignedSpindles As Double = preactor.ReadFieldDouble("Orders", "Constraint Quantity", RecordNumber, 1, 0)

            Dim dtJobMerge_s As DataTable = New DataTable()
            Dim c_Id_s As DataColumn = New DataColumn("Id", Type.[GetType]("System.Double"))
            Dim c_JobOrderNo_s As DataColumn = New DataColumn("Job Order No", Type.[GetType]("System.String"))
            Dim c_Check_s As DataColumn = New DataColumn("Check", Type.[GetType]("System.String"))
            Dim c_NoOfSpindles_s As DataColumn = New DataColumn("Allocated Spindles", Type.[GetType]("System.String"))
            Dim c_JobQty_s As DataColumn = New DataColumn("Job Quantity", Type.[GetType]("System.String"))

            dtJobMerge_s.Columns.Add(c_Id_s)
            dtJobMerge_s.Columns.Add(c_JobOrderNo_s)
            dtJobMerge_s.Columns.Add(c_Check_s)
            dtJobMerge_s.Columns.Add(c_NoOfSpindles_s)
            dtJobMerge_s.Columns.Add(c_JobQty_s)

            '' Populate the dtJobMerge table
            Dim intLengthOfJobOrderNo As Integer = strOrderNo.Length()
            Dim strSearchThis As String = "#"
            Dim intCharacterEndIndex As Integer = strOrderNo.IndexOf(strSearchThis)
            Dim strAfterHashOrderNo As String = strOrderNo.Substring(intCharacterEndIndex + 1, intLengthOfJobOrderNo - intCharacterEndIndex - 1)
            Dim intSuffixStartNo As Integer = intCharacterEndIndex + 1
            Dim strJobSuffix = strOrderNo.Substring(intSuffixStartNo, 3)
            Dim strOrderNoBeforeHash = strOrderNo.Substring(0, intCharacterEndIndex)

            '' Populating the table
            Dim intMaxRecordNo As Integer = preactor.RecordCount("Orders")
            Dim strReqOrderNumber As String = ""
            Dim strReqResource As String = ""
            Dim dteReqSetupStart As DateTime

            Dim x As Integer = 1
            Dim y As Integer = 0

            Dim dtJobMerge_sr As DataRow

            strReqOrderNumber = preactor.ReadFieldString("Orders", "Order No.", x)

            Dim dblNoOfSpindles As Double = 0
            Dim dblJobOrderQty As Double = 0

            Do

                strReqOrderNumber = preactor.ReadFieldString("Orders", "Order No.", x)
                strReqResource = preactor.ReadFieldString("Orders", "Resource", x)
                dteReqSetupStart = preactor.ReadFieldDateTime("Orders", "Setup Start", x)

                If strReqOrderNumber.ToString() IsNot "" Then
                    Dim intLengthOrderNo As Integer = strReqOrderNumber.Length()
                    Dim strfindThis As String = "#"
                    Dim intCharEndIndex As Integer = strReqOrderNumber.IndexOf(strfindThis)
                    Dim strReqOrderNumberBeforeHash = strReqOrderNumber.Substring(0, intCharEndIndex)
                    Dim strReqOrderNumberAftereHash As String = strReqOrderNumber.Substring(intCharEndIndex + 1, intLengthOrderNo - intCharEndIndex - 1)


                    '' -28-06-2022 - End

                    If strReqOrderNumberAftereHash.Length = 3 Then
                        intSuffixStartNo = strReqOrderNumber.IndexOf(strfindThis)
                        intSuffixStartNo = intSuffixStartNo + 1
                        Dim strReqSuffix = strReqOrderNumber.Substring(intSuffixStartNo, 3)

                        dblNoOfSpindles = preactor.ReadFieldDouble("Orders", "K203_AllocatedSpindles", x)
                        dblJobOrderQty = preactor.ReadFieldDouble("Orders", "Quantity", x)

                        '' preactor.WriteField("Orders", "Quantity per Hour", x, strOrderNumber)

                        If strReqOrderNumberBeforeHash = strOrderNoBeforeHash And strReqSuffix <> strJobSuffix And strReqResource = strResource And dteReqSetupStart >= dteSetupStart Then

                            dtJobMerge_sr = dtJobMerge_s.NewRow()
                            dtJobMerge_sr("Id") = y.ToString
                            dtJobMerge_sr("Job Order No") = strReqOrderNumber
                            '' dtJobMerge_sr("No. of Spindles") = intNoOfSpindles
                            dtJobMerge_sr("Allocated Spindles") = dblNoOfSpindles
                            dtJobMerge_sr("Job Quantity") = dblJobOrderQty

                            dtJobMerge_s.Rows.Add(dtJobMerge_sr)

                            y = y + 1
                        End If

                        '' x = x + 1
                    End If
                End If


                x = x + 1
            Loop While x <= intMaxRecordNo

            If y > 0 Then
                Dim oForm As New K203_JobMerge()

                '' preactor.Commit("dtJobMerge_s")
                oForm.dblTotAssignedSpindle = dblTotalAssignedSpindles
                oForm.tblJobMerge = dtJobMerge_s
                oForm.strJobOrderNo = strOrderNo
                oForm.dblJobOrderQty = dblOrderQty
                oForm.isOkClick = False

                ''show popup screen
                oForm.ShowDialog()

                Dim tbl_JobMerge As DataTable
                Dim dblTotalJobQty As Double = 0
                Dim strJobQty As String = ""

                If oForm.isOkClick = True Then
                    If Not oForm.tblJobMerge Is Nothing Then
                        tbl_JobMerge = oForm.tblJobMerge

                        Dim intSelectRecNumber As Integer
                        For Each tbl_JobMergeRow As DataRow In tbl_JobMerge.Rows
                            '' strJobQty = tbl_JobMergeRow("Job Quantity").ToString
                            '' MsgBox("strJobQty A " & strJobQty)
                            If tbl_JobMergeRow("Check").ToString() = "True" Then
                                dblTotalJobQty = dblTotalJobQty + CDbl(tbl_JobMergeRow("Job Quantity").ToString())
                            End If

                        Next
                        Dim dblAfterMergeOrderQty As Double = dblOrderQty + dblTotalJobQty

                        preactor.WriteField("Orders", "Quantity", RecordNumber, dblAfterMergeOrderQty)
                        preactor.WriteField("Orders", "K203_Original_Quantity", RecordNumber, dblAfterMergeOrderQty)
                        preactor.WriteField("Orders", "Order Type", RecordNumber, "MERGE")
                        preactor.Commit("Orders")

                        For Each tbl_JobMergeRow As DataRow In tbl_JobMerge.Rows

                            Try
                                If tbl_JobMergeRow("Check").ToString() = "True" Then
                                    dblTotalJobQty = dblTotalJobQty + CDbl(tbl_JobMergeRow("Job Quantity").ToString())


                                    intSelectRecNumber = 0

                                    intSelectRecNumber = preactor.FindMatchingRecord("Orders", "Order No.", intSelectRecNumber, tbl_JobMergeRow("Job Order No").ToString())

                                    '' MsgBox("intSelectRecNumber " & intSelectRecNumber)
                                    Dim order_No As String = preactor.ReadFieldString("Orders", "Order No.", intSelectRecNumber)
                                    Dim aPSOprSeq As Integer = preactor.ReadFieldInt("Orders", "K203_APSOprSeq", intSelectRecNumber)
                                    Dim eRPJobNum As String = preactor.ReadFieldString("Orders", "K203_ERPJobNum.", intSelectRecNumber)
                                    Dim eRPOprSeq As Integer = preactor.ReadFieldInt("Orders", "K203_ERPOprSeq", intSelectRecNumber)
                                    Dim partNo As String = preactor.ReadFieldString("Orders", "Part No.", intSelectRecNumber)
                                    Dim quantity As Double = preactor.ReadFieldDouble("Orders", "Quantity", intSelectRecNumber)

                                    'Dim dOrdernum As Integer = preactor.CreateRecord("K203_DeleteOrder")
                                    ''Asign "dragtemp" table to resource and setup start time
                                    ''preactor.WriteField("K203_DeleteOrders", "RecordId.", dOrdernum, 1)
                                    'preactor.WriteField("K203_DeleteOrder", "ERPJobNum.", dOrdernum, eRPJobNum)
                                    'preactor.WriteField("K203_DeleteOrder", "ERPOprSeq", dOrdernum, eRPOprSeq)
                                    'preactor.WriteField("K203_DeleteOrder", "APSOrder No.", dOrdernum, order_No)
                                    'preactor.WriteField("K203_DeleteOrder", "APSOprSeq", dOrdernum, eRPOprSeq)
                                    'preactor.WriteField("K203_DeleteOrder", "Part No.", dOrdernum, partNo)
                                    'preactor.WriteField("K203_DeleteOrder", "Quantity", dOrdernum, quantity)
                                    'preactor.Commit("K203_DeleteOrder")

                                    Console.WriteLine("After commit the Delete order table. ")
                                    '' To delete the order
                                    preactor.DeleteRecord("Orders", intSelectRecNumber)
                                    preactor.Commit("Orders")
                                    '' MsgBox(" GGGGG ")
                                End If
                            Catch ex As Exception
                                MsgBox(ex.Message)
                            End Try


                        Next

                        preactor.Commit("Orders")
                        '' preactor.Redraw()

                        MsgBox("Successfuly Completed")

                    End If
                End If
            Else
                MsgBox("No jobs for merging")
            End If


        End If
        Return 0
    End Function

#Region "Gant Chart Drag and Drop"

    ''In Gant chart if user selected block drop this event will execute
    Public Function K203_GatChartBlockDrop(ByRef preactorComObject As PreactorObj, ByRef pespComObject As Object, ByRef i As Integer) As Integer
        Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)
        Dim setup_Start As Date = preactor.ReadFieldDateTime("Orders", "Setup Start", i)

        Dim revised_Supply_Date As Date = preactor.ReadFieldDateTime("Orders", "K203_RevisedSupplyDate", i)
        If revised_Supply_Date.Year > 2000 Then
            If (revised_Supply_Date <= setup_Start) Then
                ''preactor.WriteField("Orders", "Earliest Start Date", i, setup_Start)
                preactor.WriteField("Orders", "Earliest Start Date", i, revised_Supply_Date)
                ''MsgBox("RM date Updated",, "Information")
            End If
        End If

        Return 0
    End Function
#End Region

    Public Function GetOrderSerialSeq(ByRef connetionString As String, ByRef erpJobNo As String) As Integer

        Try
            Dim connection As SqlConnection
            Dim adapter As SqlDataAdapter
            Dim command As New SqlCommand

            connection = New SqlConnection(connetionString)

            connection.Open()
            command.Connection = connection
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "K203_GetOrderSerialSeq_Sp"
            command.CommandTimeout = 600

            Dim param As SqlParameter

            param = New SqlParameter("@ErpJobNo", erpJobNo)
            param.Direction = ParameterDirection.Input
            param.DbType = DbType.String
            command.Parameters.Add(param)

            Dim OrderSerialSeq As Integer
            param = New SqlParameter("@OrderSerialSeq", OrderSerialSeq)
            param.Direction = ParameterDirection.Output
            param.DbType = DbType.Double
            '' param.DbType = DbType.Decimal
            command.Parameters.Add(param)

            adapter = New SqlDataAdapter(command)
            command.ExecuteNonQuery()
            ''MsgBox("CDec(param.Value) " & CDec(param.Value))
            ''MsgBox("param.Value.ToString " & param.Value.ToString)
            If Not (param.Value.ToString = "") Then
                '' Return CInt(param.Value)
                Return CInt(param.Value)
            Else
                Return 0
            End If
            connection.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

        End Try
    End Function

    Public Function K203_GetOrderChangeStatus(ByRef preactorComObject As PreactorObj, ByRef pespComObject As Object, ByRef RecordNumber As Integer) As Integer

        Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)
        Dim planningboard As IPlanningBoard = preactor.PlanningBoard
        ''  Dim isOrderChange As Boolean = planningboard.get(RecordNumber)

        ''Dim orno As String = preactor.ReadFieldString("Orders", "Order No.", pespComObject
        ''
        Dim dblOriginal_Quantity As Double = preactor.ReadFieldDouble("Orders", "K203_Original_Quantity", RecordNumber)
        Dim dblQuantity As Double = preactor.ReadFieldDouble("Orders", "Quantity", RecordNumber)
        If Not (dblQuantity = dblOriginal_Quantity) Then
            preactor.WriteField("Orders", "K203_APS_UpdateStatus", RecordNumber, "M")
            preactor.WriteField("Orders", "K203_Original_Quantity", RecordNumber, dblQuantity)
        End If
        ''  MsgBox(isOrderChange)
        Return 0
    End Function

    Public Function K203_ReduceQuantityBulk(ByRef preactorComObject As PreactorObj, ByRef pespComObject As Object, ByRef RecordNumber As Integer) As Integer

        Dim preactor As IPreactor = PreactorFactory.CreatePreactorObject(preactorComObject)
        Dim planningboard As IPlanningBoard = preactor.PlanningBoard
        Dim connetionString As String = preactor.ParseShellString("{DB CONNECT STRING}")

        Dim strERPJobNum As String = preactor.ReadFieldString("Orders", "K203_ERPJobNum.", RecordNumber)
        Dim strOrderNo As String = preactor.ReadFieldString("Orders", "Order No.", RecordNumber)
        Dim decReducedQuantityMain As Double = preactor.ReadFieldDouble("Orders", "Numerical Attribute 3", RecordNumber)


        Dim tbl_ReduceQuantiy As DataTable = New DataTable()
        Dim rowId As DataColumn = New DataColumn("ID", Type.[GetType]("System.Double"))
        tbl_ReduceQuantiy.Columns.Add(rowId)
        Dim sOrderNo As DataColumn = New DataColumn("OrderNo", Type.[GetType]("System.String"))
        tbl_ReduceQuantiy.Columns.Add(sOrderNo)
        Dim sOrderQuantity As DataColumn = New DataColumn("OrderQuantity", Type.[GetType]("System.Double"))
        tbl_ReduceQuantiy.Columns.Add(sOrderQuantity)
        Dim sReducedMainQuantity As DataColumn = New DataColumn("ReducedMainQuantity", Type.[GetType]("System.Double"))
        tbl_ReduceQuantiy.Columns.Add(sReducedMainQuantity)
        Dim sReducedQuantity As DataColumn = New DataColumn("ReducedQuantity", Type.[GetType]("System.Double"))
        tbl_ReduceQuantiy.Columns.Add(sReducedQuantity)
        Dim sFinalQuantity As DataColumn = New DataColumn("FinalQuantity", Type.[GetType]("System.Double"))
        tbl_ReduceQuantiy.Columns.Add(sFinalQuantity)

        Dim strERPJobNumTemp As String
        Dim strOrderNoTemp As String
        Dim strQuantityTemp As Double
        Dim strReducedMainQuantityTemp As Double
        Dim strReducedQuantityTemp As Double
        Dim strFinalQuantityTemp As Double

        Dim orderCount As Integer = preactor.RecordCount("Orders")
        Dim x As Integer = 1
        Dim id As Integer = 0

        Do
            strERPJobNumTemp = preactor.ReadFieldString("Orders", "K203_ERPJobNum.", x)

            If (strERPJobNum = strERPJobNumTemp) Then
                strOrderNoTemp = preactor.ReadFieldString("Orders", "Order No.", x)
                strQuantityTemp = preactor.ReadFieldDouble("Orders", "Quantity", x)
                strReducedMainQuantityTemp = preactor.ReadFieldDouble("Orders", "Numerical Attribute 3", x)
                strFinalQuantityTemp = preactor.ReadFieldDouble("Orders", "Quantity", x)
                id = id + 1
                strReducedQuantityTemp = 0
                tbl_ReduceQuantiy.Rows.Add(id, strOrderNoTemp, strQuantityTemp, strReducedMainQuantityTemp, strReducedQuantityTemp, strFinalQuantityTemp)
            End If

            x = x + 1
        Loop While x <= orderCount

        Dim oReduceQuantiyBulk As New K203_ReduceQuantiyBulk()
        oReduceQuantiyBulk.strConnectionCon = connetionString
        oReduceQuantiyBulk.strERPJobNum = strERPJobNum
        oReduceQuantiyBulk.decReduceQuantiyMain = decReducedQuantityMain
        oReduceQuantiyBulk.tblReduceQuantiy = tbl_ReduceQuantiy

        oReduceQuantiyBulk.ShowDialog()
        tbloReduceQuantiyBulk = oReduceQuantiyBulk.tblReduceQuantiy_Calculated


        Dim orderNo As String
        Dim reducedQuantity As Double
        Dim order_Num As Integer = 0
        Dim order_Quantity As Double = 0

        Try
            If Not ((tbloReduceQuantiyBulk Is DBNull.Value) Or (tbloReduceQuantiyBulk Is Nothing)) Then
                For Each rqbrow As DataRow In tbloReduceQuantiyBulk.Rows
                    orderNo = rqbrow("OrderNo").ToString()
                    reducedQuantity = CDbl(rqbrow("ReducedQuantity").ToString())

                    order_Num = preactor.FindMatchingRecord("Orders", "Order No.", order_Num, orderNo)
                    order_Quantity = preactor.ReadFieldDouble("Orders", "Quantity", order_Num)

                    If order_Num > 0 Then
                        preactor.WriteField("Orders", "K203_Order_Reduced_Qty", order_Num, reducedQuantity)
                        preactor.WriteField("Orders", "Quantity", order_Num, (order_Quantity - reducedQuantity))
                    End If
                    order_Num = preactor.FindMatchingRecord("Orders", "Order No.", order_Num, orderNo)

                Next
                preactor.Commit("Orders")
            End If

        Catch ex As Exception

        End Try
            Return 0
    End Function

End Class
