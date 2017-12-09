import React, { PureComponent } from 'react';
import { ButtonDropdown, DropdownToggle } from 'reactstrap';

export default class PageHeader extends PureComponent {
  constructor(props) {
    super(props);
    this.state = {
      dropdownOpen: false
    };
  }

  toggle = () => {
    this.setState({
      dropdownOpen: !this.state.dropdownOpen
    });
  }

  render() {
    return (
      <div
        style={{ display: 'inline-block', margin: '0.5rem 0', width: '100%' }}
      >
        <h3 className="float-left">{this.props.title || 'No title'}</h3>
        <span className="float-right">
          {this.props.dropdownMenus && (
            <ButtonDropdown
              isOpen={this.state.dropdownOpen}
              toggle={this.toggle}
            >
              <DropdownToggle caret>
                <i className="fa fa-cogs" aria-hidden="true" />
              </DropdownToggle>
              {this.props.dropdownMenus}
            </ButtonDropdown>
          )}
          {this.props.mainActions || ''}
        </span>
      </div>
    );
  }
}
