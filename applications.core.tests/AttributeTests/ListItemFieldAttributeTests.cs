using Applications.Core.Attributes;
using Applications.Core.Models;
using Xunit;

namespace Applications.Core.Tests.AttributeTests
{

    public class ListItemFieldAttributeTests
    {
        [Fact]
        public void Values_Set_In_Constructor_Should_Be_Set_Correctly()
        {
            var enableSorting = true;
            var caption = "Caption";
            var displayOrder = 3;
            var sortFieldName = "SortField";
            var hideCaption = true;
            var hideEmptyColumn = true;
            var attribute = new ListColumnAttribute(
                enableSorting: enableSorting,
                headerText: caption,
                displayOrder: displayOrder,
                sortColumnName: sortFieldName
                )
            {
                HideCaption = hideCaption,
                HideEmptyColumn = hideEmptyColumn,
            };

            var result = attribute.EnableSorting == enableSorting &&
                attribute.HeaderText == caption &&
                attribute.DisplayOrder == displayOrder &&
                attribute.SortColumnName == sortFieldName &&
                attribute.HideCaption == hideCaption &&
                attribute.HideEmptyColumn == hideEmptyColumn;

            Assert.True(result);

            enableSorting = false;
            caption = "";
            displayOrder = 0;
            sortFieldName = "";
            hideCaption = false;
            hideEmptyColumn = false;
            attribute = new ListColumnAttribute(
                enableSorting: enableSorting,
                headerText: caption,
                displayOrder: displayOrder,
                sortColumnName: sortFieldName
                )
            {
                HideCaption = hideCaption,
                HideEmptyColumn = hideEmptyColumn,
            };

            result = attribute.EnableSorting == enableSorting &&
                attribute.HeaderText == caption &&
                attribute.DisplayOrder == displayOrder &&
                attribute.SortColumnName == sortFieldName &&
                attribute.HideCaption == hideCaption &&
                attribute.HideEmptyColumn == hideEmptyColumn;

            Assert.True(result);

            attribute = new ListColumnAttribute(
                enableSorting: enableSorting,
                headerText: caption,
                displayOrder: displayOrder,
                sortColumnName: sortFieldName
                );

            result = attribute.EnableSorting == enableSorting &&
                attribute.HeaderText == caption &&
                attribute.DisplayOrder == displayOrder &&
                attribute.SortColumnName == sortFieldName &&
                attribute.HideCaption == false &&
                attribute.HideEmptyColumn == false;

            Assert.True(result);
        }
    }
}
