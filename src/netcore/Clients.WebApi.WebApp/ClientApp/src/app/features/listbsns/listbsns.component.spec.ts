import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListBsnsComponent } from './listbsns.component';

describe('ListbsnsComponent', () => {
  let component: ListBsnsComponent;
  let fixture: ComponentFixture<ListBsnsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListBsnsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListBsnsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
