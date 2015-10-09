﻿//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ExcelStepFormatter.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using ClosedXML.Excel;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Excel
{
    public class ExcelStepFormatter
    {
        private readonly ExcelDocumentStringFormatter excelDocumentStringFormatter;
        private readonly ExcelTableFormatter excelTableFormatter;

        public ExcelStepFormatter(
            ExcelTableFormatter excelTableFormatter,
            ExcelDocumentStringFormatter excelDocumentStringFormatter)
        {
            this.excelTableFormatter = excelTableFormatter;
            this.excelDocumentStringFormatter = excelDocumentStringFormatter;
        }

        public void Format(IXLWorksheet worksheet, Step step, ref int row)
        {
            worksheet.Cell(row, "C").Style.Font.SetBold();
            worksheet.Cell(row, "C").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            worksheet.Cell(row, "C").Value = step.NativeKeyword;
            worksheet.Cell(row++, "D").Value = step.Name;

            if (step.TableArgument != null)
            {
                this.excelTableFormatter.Format(worksheet, step.TableArgument, ref row);
            }

            if (!string.IsNullOrEmpty(step.DocStringArgument))
            {
                this.excelDocumentStringFormatter.Format(worksheet, step.DocStringArgument, ref row);
            }
        }
    }
}
