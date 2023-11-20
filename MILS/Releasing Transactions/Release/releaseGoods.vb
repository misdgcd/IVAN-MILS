﻿Public Class releaseGoods
    Private q As New qry
    'Private table As New DataTable("Table")
    Public docTypeId As String
    Public docRefTypeId As String
    Public receiverId As String
    Private slctedRow As Integer

    Private Sub releaseGoods_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        newDGVFormat()
        loadForm()

    End Sub

    Sub loadForm()
        loadTbxDocType()
        loadtbxRefDocType()
        q.loadSender(cbxReceiver)
        cbxOwnership.SelectedIndex = 0
    End Sub

    Private Sub dgvRels_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRels.CellDoubleClick
        Dim row As Integer = dgvRels.CurrentCell.RowIndex
        If e.ColumnIndex = 1 Then
            Home_Page.Enabled = False
            Me.Enabled = False
            With selectProducts
                .rel = True
                .loadLV()
                .Show()
                .targetRow1 = row
            End With
        End If
    End Sub

    Sub newDGVFormat()


        With dgvRels

            .AllowUserToAddRows = True

            .ColumnHeadersHeight = 45
            .RowTemplate.Height = 35

            .Columns.Add("0", "id")
            .Columns.Add("1", "PRODUCT NUMBER")
            .Columns.Add("2", "DESCRIPTION")
            .Columns.Add("3", "BATCH CODE")
            .Columns.Add("4", "LOCATION")
            .Columns.Add("5", "QUANTITY")
            .Columns.Add("6", "bId")
            .Columns.Add("7", "lId")
            .Columns.Add("8", "EXPIRATION DATE")

            .Columns(0).Visible = False
            .Columns(6).Visible = False
            .Columns(7).Visible = False

            .Columns(1).Width = 150
            .Columns(2).Width = 523
            .Columns(3).Width = 95
            .Columns(4).Width = 95
            .Columns(5).Width = 100
            .Columns(8).Width = 175

            .Columns(0).ReadOnly = True
            .Columns(1).ReadOnly = True
            .Columns(2).ReadOnly = True
            .Columns(3).ReadOnly = True
            .Columns(4).ReadOnly = True
            .Columns(5).ReadOnly = False
            .Columns(6).ReadOnly = True
            .Columns(7).ReadOnly = True
            .Columns(8).ReadOnly = True


            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            For Index As Integer = 0 To .ColumnCount - 1
                .Columns(Index).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns(Index).HeaderCell.Style.Font = New Font(.ColumnHeadersDefaultCellStyle.Font.FontFamily, 8, FontStyle.Bold)
            Next


        End With
    End Sub


    Private Sub dgvRels_Click(sender As Object, e As EventArgs) Handles dgvRels.Click
        If dgvRels.Rows.Count = 0 Then
            l5.Visible = True
            l5.ForeColor = Color.Red
            l5.Text = "Please select valid data..."
        Else
            If dgvRels.Rows.Count > 1 Then
                l5.Visible = True
                slctedRow = dgvRels.CurrentCell.RowIndex
            End If
        End If
    End Sub

    Sub loadTbxDocType()
        With tbxDocType
            .Text = ""
            .Refresh()
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
            Dim col As New AutoCompleteStringCollection()
            'q.suggestDocType(col)
            q.suggestDocTypeRels(col)
            .AutoCompleteCustomSource = col
        End With
    End Sub

    Sub loadtbxRefDocType()
        With tbxRefDocType
            .Text = ""
            .Refresh()
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
            Dim col As New AutoCompleteStringCollection()
            'q.suggestRefDocType(col)
            q.suggestRefDocTypeRels(col)
            .AutoCompleteCustomSource = col
        End With
    End Sub

    Private Sub cbxReceiver_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbxReceiver.SelectedValueChanged
        receiverId = ""
        receiverId = q.fetchSenderId(cbxReceiver.Text)
    End Sub

    Private Sub tbxDocNum_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbxDocNum.KeyPress
        'If Asc(e.KeyChar) <> 8 Then
        '    If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
        '        e.Handled = True
        '    End If
        'End If
    End Sub

    Private Sub tbxRefDocNum_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbxRefDocNum.KeyPress
        'If Asc(e.KeyChar) <> 8 Then
        '    If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
        '        e.Handled = True
        '    End If
        'End If
    End Sub

    Private Sub tbxDocType_Leave(sender As Object, e As EventArgs) Handles tbxDocType.Leave
        If Not String.IsNullOrWhiteSpace(tbxDocType.Text) Then
            'q.fetchIdDocType(tbxDocType.Text)
            q.fetchIdDocTypeRels(tbxDocType.Text)
            'MsgBox(docTypeId)
        Else

        End If

        If Not String.IsNullOrWhiteSpace(tbxDocType.Text) And Not String.IsNullOrWhiteSpace(tbxDocNum.Text) Then
            ValidateDocumentNumber()
        End If
    End Sub

    Private Sub tbxRefDocType_Leave(sender As Object, e As EventArgs) Handles tbxRefDocType.Leave
        If Not String.IsNullOrWhiteSpace(tbxDocType.Text) Then
            'q.fetchIdDocType(tbxDocType.Text)
            q.fetchIdRefDocTypeRels(tbxRefDocType.Text)
            'MsgBox(docRefTypeId)
        Else

        End If
    End Sub

    'Private Sub dgvRels_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles dgvRels.CellValidating
    '    Dim row As DataGridViewRow = dgvRels.Rows(e.RowIndex)
    '    If e.ColumnIndex = 5 Then
    '        With dgvRels
    '            If .IsCurrentCellDirty Then
    '                If Not IsNumeric(e.FormattedValue) Then
    '                    e.Cancel = True
    '                    MessageBox.Show("Insert valid quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                ElseIf e.FormattedValue = 0 Or e.FormattedValue < 0 Then
    '                    e.Cancel = True
    '                    'row.Cells(6).Value = emptyCell
    '                    MessageBox.Show("Insert valid quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                End If
    '            End If
    '        End With
    '    End If
    'End Sub



    Private Sub dgvRels_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvRels.RowValidating
        Dim row As DataGridViewRow = dgvRels.Rows(e.RowIndex)
        Dim isValidQty As Boolean = True
        Dim repeatingId As Boolean = False
        Dim isEmpty As Boolean

        Try
            With dgvRels
                If .IsCurrentRowDirty Then
                    Dim idDGV As String = ""
                    idDGV = row.Cells(0).Value.ToString
                    Dim qtyDGV As String = ""
                    qtyDGV = row.Cells(5).Value.ToString

                    For Each cell As DataGridViewCell In row.Cells 'is null
                        If String.IsNullOrWhiteSpace(cell.Value.ToString) Then
                            e.Cancel = True
                            isEmpty = True
                        End If
                    Next

                    For i As Integer = 0 To Me.dgvRels.RowCount - 1
                        For j As Integer = 0 To Me.dgvRels.RowCount - 1
                            If i <> j Then
                                If dgvRels.Rows(i).Cells(0).Value = dgvRels.Rows(j).Cells(0).Value Then
                                    'dgvRels.Rows(i).DefaultCellStyle.BackColor = Color.Red
                                    e.Cancel = True
                                    l5.Visible = True
                                    l5.Text = "Duplicate data are not valid."
                                    repeatingId = True
                                Else
                                    'dgvRels.Rows(i).DefaultCellStyle.BackColor = Color.White
                                End If
                            End If
                        Next
                    Next

                    Try
                        isValidQty = q.validationQuantityProd(idDGV, qtyDGV) ' check the quantity
                    Catch ex As Exception
                        MessageBox.Show("Fields with * are required.", "Error",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error)
                        l1.Visible = True
                        l2.Visible = True
                        l3.Visible = True
                        l4.Visible = True
                        l5.Visible = True
                        Label5.Visible = True
                        Label7.Visible = True
                        Exit Sub
                    End Try
                    If isEmpty = True Then
                        e.Cancel = True
                        MessageBox.Show("Fields with * are required.", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ElseIf isValidQty = False Then
                        e.Cancel = True
                        MessageBox.Show("You do not have enough of these items.", "Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ElseIf repeatingId = False Then
                        l5.Visible = True
                    End If
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub releaseGoods_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If dgvRels.Rows.Count > 1 Then
            If e.KeyCode = Keys.Delete Then
                Dim result As Integer = MessageBox.Show("Are you sure want to remove selected row?", "Remove Details", MessageBoxButtons.YesNo)
                If result = DialogResult.No Then
                    'Exit Sub
                ElseIf result = DialogResult.Yes Then
                    Try
                        dgvRels.Rows.RemoveAt(slctedRow)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            End If

            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Else
            If e.KeyCode = Keys.Escape Then

                Me.Close()
            End If
        End If
    End Sub



    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If btnAdd.Text = "Record" Then

            If (MessageBox.Show("Do You Want To Record?", "Confirmation", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No) Then

            Else
                validateFields()
            End If
        ElseIf btnAdd.Text = "Add New Entry" Then
            With Me
                .dgvRels.AllowUserToAddRows = True
                .dgvRels.Enabled = True
            End With
            clearfields()
            cbxReceiver.Select()
            btnAdd.Text = "Record"
            l5.Visible = True
            newDGVFormat()
        End If
    End Sub

    Sub clearfields()
        cbxReceiver.SelectedIndex = -1
        tbxDocType.Text = ""
        docTypeId = ""
        tbxDocNum.Text = ""
        TextBox1.Text = ""
        tbxEntry.Text = ""
        tbxRefDocType.Text = ""
        docRefTypeId = ""
        tbxRefDocNum.Text = ""
        cbxOwnership.SelectedIndex = 0
        dtpRefDate.Value = Date.Now
        dgvRels.Rows.Clear()
        dgvRels.Columns.Clear()

    End Sub

    Sub validateFields()
        Dim dgvCount As Integer = dgvRels.Rows.Count - 1
        If cbxReceiver.SelectedIndex = -1 Or String.IsNullOrWhiteSpace(tbxDocType.Text) _
            Or String.IsNullOrWhiteSpace(tbxDocNum.Text) Or String.IsNullOrWhiteSpace(tbxRefDocType.Text) _
            Or String.IsNullOrWhiteSpace(TextBox1.Text) Or dgvCount = 0 Then
            MessageBox.Show("Fields with '*' are required", "Info:", MessageBoxButtons.OK)
            l1.Visible = True
            l2.Visible = True
            l3.Visible = True
            l4.Visible = True
            l5.Visible = True
            Label5.Visible = True
            Label7.Visible = True
            Exit Sub

        Else

            q.addRelsTransaction(docTypeId, tbxDocNum.Text, dtpRefDate.Value, newHome.userId, receiverId,
                                     TextBox1.Text, cbxOwnership.Text, docRefTypeId, tbxRefDocNum.Text, newHome.areaId)
        End If

    End Sub

    Private Sub releaseGoods_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If btnAdd.Text = "Add New Entry" Then
            e.Cancel = False

        Else
            If Not cbxReceiver.SelectedIndex = -1 Or Not String.IsNullOrWhiteSpace(tbxDocType.Text) _
            Or Not String.IsNullOrWhiteSpace(tbxDocNum.Text) Or Not String.IsNullOrWhiteSpace(tbxRefDocType.Text) _
            Or dgvRels.Rows.Count > 0 Then
                If (MessageBox.Show("Are you sure you want to cancel this transaction?", "Info", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No) Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                    clearfields()
                End If
            Else
                e.Cancel = False
            End If

        End If

    End Sub

    Sub ValidateDocumentNumber()
        q.validateDocumentRelsTransaction(tbxDocType.Text, docTypeId, tbxDocNum.Text, newHome.areaId)
    End Sub

    Private Sub tbxDocNum_Leave(sender As Object, e As EventArgs) Handles tbxDocNum.Leave
        If Not String.IsNullOrWhiteSpace(tbxDocType.Text) And Not String.IsNullOrWhiteSpace(tbxDocNum.Text) Then
            ValidateDocumentNumber()
        End If
    End Sub

    Private Sub CbxReceiver_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxReceiver.SelectedIndexChanged

    End Sub
End Class