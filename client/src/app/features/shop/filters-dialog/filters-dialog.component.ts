import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ShopService } from '../../../core/services/shop.service';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatButton } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
@Component({
  selector: 'app-filters-dialog',
  imports: [
    FormsModule,
    MatDivider,
    MatSelectionList,
    MatListOption,
    MatButton,
  ],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.scss',
})
export class FiltersDialogComponent {
  shopService = inject(ShopService);
  private dialogRef = inject(MatDialogRef<FiltersDialogComponent>);
  data = inject(MAT_DIALOG_DATA);

  selectedBrands: string[] = this.data.selectedBrands;
  selectedTypes: string[] = this.data.selectedTypes;

  applyFilters() {
    this.dialogRef.close({
      selectedBrands: this.selectedBrands,
      selectedTypes: this.selectedTypes,
    });
  }
}
