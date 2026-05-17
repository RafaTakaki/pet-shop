import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImagemPadraoComponent } from './imagem-padrao.component';

describe('ImagemPadraoComponent', () => {
  let component: ImagemPadraoComponent;
  let fixture: ComponentFixture<ImagemPadraoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ImagemPadraoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ImagemPadraoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
