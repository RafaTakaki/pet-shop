import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarMeusPetsComponent } from './listar-meus-pets.component';

describe('ListarMeusPetsComponent', () => {
  let component: ListarMeusPetsComponent;
  let fixture: ComponentFixture<ListarMeusPetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListarMeusPetsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListarMeusPetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
