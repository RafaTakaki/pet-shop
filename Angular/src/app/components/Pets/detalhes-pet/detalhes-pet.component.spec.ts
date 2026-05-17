import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesPetComponent } from './detalhes-pet.component';

describe('DetalhesPetComponent', () => {
  let component: DetalhesPetComponent;
  let fixture: ComponentFixture<DetalhesPetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesPetComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalhesPetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
