import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComprobanteCompraComponent } from './comprobante-compra.component';

describe('ComprobanteCompraComponent', () => {
  let component: ComprobanteCompraComponent;
  let fixture: ComponentFixture<ComprobanteCompraComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ComprobanteCompraComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ComprobanteCompraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
